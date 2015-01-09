using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoiseTest
{
    public partial class ComparisonForm : Form
    {
        private Thread _workerThread;
        private SampleMode _mode;
        private int _octaves;
        private int _size;
        private double _scale;
        private double _persistence;
        private double _lacunarity;
        private double _power;
        private bool _absoluteValue;
        private bool _normalize;

        public ComparisonForm()
        {
            InitializeComponent();
        }

        private void ComparisonForm_Load(object sender, EventArgs e)
        {
            comboBoxMode.SelectedIndex = 0;
        }

        private static double GetDoubleValue(TextBox textBox, string name, double? min = null, double? max = null)
        {
            var stringValue = textBox.Text;
            double result;
            if (!Double.TryParse(stringValue, out result))
            {
                throw new Exception(String.Format("Unable to parse field: {0}\nExpecting integer value.", name));
            }
            if (max != null && result > max)
            {
                throw new Exception(String.Format("Input error for field: {0}\nExpecting integer value less than {1}.", name, max));
            }
            if (min != null && result < min)
            {
                throw new Exception(String.Format("Input error for field: {0}\nExpecting integer value greater than {1}.", name, min));
            }
            return result;
        }

        private static int GetIntValue(TextBox textBox, string name, int? min = null, int? max = null)
        {
            var stringValue = textBox.Text;
            int result;
            if (!Int32.TryParse(stringValue, out result))
            {
                throw new Exception(String.Format("Unable to parse field: {0}\nExpecting integer value.", name));
            }
            if (max != null && result > max)
            {
                throw new Exception(String.Format("Input error for field: {0}\nExpecting integer value less than {1}.", name, max));
            }
            if (min != null && result < min)
            {
                throw new Exception(String.Format("Input error for field: {0}\nExpecting integer value greater than {1}.", name, min));
            }
            return result;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            if (_workerThread != null)
            {
                _workerThread.Abort();
                _workerThread = null;
                buttonGenerate.Text = "Generate Images";
                return;
            }
            try
            {
                _octaves = GetIntValue(textBoxOctaves, "Octaves", 0, 51);
                _scale = GetDoubleValue(textBoxScale, "Scale", 0.0);
                _persistence = GetDoubleValue(textBoxPersistence, "Persistence", 0.0);
                _lacunarity = GetDoubleValue(textBoxLacunarity, "Lacunarity", 0.0);
                _size = GetIntValue(textBoxSize, "Size", 0, 10001);
                _power = GetDoubleValue(textBoxPower, "Power", 0.0);
                _mode = (SampleMode)comboBoxMode.SelectedIndex;
                _absoluteValue = checkBoxAbsoluteValue.Checked;
                _normalize = checkBoxNormalize.Checked;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _workerThread = new Thread(DoWork);
            // Give higher thread priority for more consistent benchmarks.
            _workerThread.Priority = ThreadPriority.AboveNormal;
            _workerThread.IsBackground = true;
            _workerThread.Start();

            pictureBoxSimplex.Image = null;
            pictureBoxSimpletic.Image = null;
            pictureBoxOpenSimplex.Image = null;

            buttonSaveSimplex.Enabled = false;
            buttonSaveSimpletic.Enabled = false;
            buttonSaveOpenSimplex.Enabled = false;
            buttonSaveComparison.Enabled = false;

            textBoxSimplex.Text = "Loading...";
            textBoxSimpletic.Text = "Loading...";
            textBoxOpenSimplex.Text = "Loading...";
            
            buttonGenerate.Text = "Cancel";
        }

        private void DoWork()
        {
            GenerateImage(NoiseProviders.Simplex, pictureBoxSimplex, textBoxSimplex, buttonSaveSimplex);
            GenerateImage(NoiseProviders.Simpletic, pictureBoxSimpletic, textBoxSimpletic, buttonSaveSimpletic);
            GenerateImage(NoiseProviders.OpenSimplex, pictureBoxOpenSimplex, textBoxOpenSimplex, buttonSaveOpenSimplex);
            Action finished = () => { _workerThread = null; buttonGenerate.Text = "Generate Images"; buttonSaveComparison.Enabled = true; };
            Invoke(finished);
        }

        private IFunction2D CreateNoiseFunction(INoiseProvider provider, Random rng)
        {
            if (_octaves == 1 && _power == 1.0 && !_absoluteValue)
            {
                // Simple case that doesn't need fBm, so we can just return a basic noise function
                // to remove some overhead for benchmarking.

                switch (_mode)
                {
                    case SampleMode.Sample2D:
                        return provider.Create2D(rng, _scale);
                    case SampleMode.Slice3D:
                        return provider.Slice3D(rng, _scale);
                    case SampleMode.Slice4D:
                        return provider.Slice4D(rng, _scale);
                    case SampleMode.Tileable2D:
                        return provider.Create2D(rng, _scale, _size, _size);
                    default:
                        throw new InvalidOperationException();
                }
            }

            Func<double, double> modifier = null;

            if (_absoluteValue)
            {
                if (_power == 1.0)
                {
                    modifier = input => (1.0 - Math.Abs(input)) * 2.0 - 1.0;
                }
                else
                {
                    modifier = input => Math.Pow(1.0 - Math.Abs(input), _power) * 2.0 - 1.0;
                }
            }
            else if (_power != 1.0)
            {
                modifier = input => input < 0 ? -Math.Pow(-input, _power) : Math.Pow(input, _power);
            }

            return provider.FractionalBrownianMotion(rng, _scale, _octaves, _persistence, _lacunarity, modifier, _size, _size, _mode);
        }

        private void GenerateImage(INoiseProvider provider, PictureBox pictureBox, TextBox diagnosticInfo, Button saveButton)
        {
            var rng = new Random();
            Bitmap bitmap;
            try
            {
                var function = CreateNoiseFunction(provider, rng);
                var heightMap = new double[_size, _size];
                var watch = new Stopwatch();
                watch.Start();
                function.FillHeightMap(heightMap);
                watch.Stop();
               
                var seconds = watch.Elapsed.TotalSeconds;
                var perPoint = (int)Math.Floor((seconds / (_octaves * _size * _size)) * 1e9);
                var min = Double.MaxValue;
                var max = Double.MinValue;
                var total = 0.0;
                for (var x = 0; x < _size; x++)
                {
                    for (var y = 0; y < _size; y++)
                    {
                        var value = heightMap[x, y];
                        total += value;
                        min = Math.Min(value, min);
                        max = Math.Max(value, max);
                    }
                }
                var average = total / (_size * _size);
                var diagnostic = String.Format("Range: {0:f4} -> {1:f4}  Avg: {2:f4}\r\n Time: {3:f4}s      Per Point: {4}ns", min, max, average, seconds, perPoint);

                if (_normalize)
                {
                    var change = max - min;
                    var offset = min + 1.0;
                    for (var x = 0; x < _size; x++)
                    {
                        for (var y = 0; y < _size; y++)
                        {
                            var value = heightMap[x, y];
                            var magnitude = (value + 1.0 - offset) / change;
                            heightMap[x, y] = magnitude * 2.0 - 1.0;
                        }
                    }
                }

                bitmap = heightMap.RenderBitmap();
                Action setBitmap = () => { pictureBox.Image = bitmap; saveButton.Enabled = true; saveButton.Tag = bitmap; };
                Invoke(setBitmap);

                Action setText = () => { diagnosticInfo.Text = diagnostic; };
                Invoke(setText);
            }
            catch
            {
                Action setText = () => { diagnosticInfo.Text = "Error generating image!"; };
                Invoke(setText);
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".png";
            sfd.Filter = "PNG Files|*.png";
            sfd.FileName = "Noise.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                (button.Tag as Image).Save(sfd.FileName, ImageFormat.Png);
            }
        }

        private void buttonSaveComparison_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = ".png";
            sfd.Filter = "PNG Files|*.png";
            sfd.FileName = "Comparison.png";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var output = new Bitmap(_size * 3, _size);
                using (Graphics g = Graphics.FromImage(output))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    RenderComparisonInfo(g, new PointF(0, 0), "Simplex Noise", pictureBoxSimplex, textBoxSimplex);
                    RenderComparisonInfo(g, new PointF(_size, 0), "Simpletic Noise", pictureBoxSimpletic, textBoxSimpletic);
                    RenderComparisonInfo(g, new PointF(_size * 2, 0), "Open Simplex Noise", pictureBoxOpenSimplex, textBoxOpenSimplex);
                }
                output.Save(sfd.FileName, ImageFormat.Png);
            }
        }

        private void RenderComparisonInfo(Graphics g, PointF offset, string header, PictureBox pictureBox, TextBox diagnosticInfo)
        {
            g.DrawImage(pictureBox.Image, offset);
            var headerFont = new Font(FontFamily.GenericSansSerif, 16.0f, FontStyle.Bold);
            var font = new Font(FontFamily.GenericMonospace, 9.0f);
            var diagnostic = diagnosticInfo.Text;
            var diagnosticSize = g.MeasureString(diagnostic, font);
            var headerSize = g.MeasureString(header, headerFont);
            var brush = new SolidBrush(Color.FromArgb(128, 0x66, 0x66, 0x66));
            var margin = 5f;
            var padding = 5f;
            var x = offset.X + margin;
            var y = offset.Y + margin;
            g.FillRectangle(brush, x, y, diagnosticSize.Width + padding * 2, headerSize.Height + padding * 2);
            g.DrawString(header, headerFont, Brushes.White, x + padding, y + padding);
            y += headerSize.Height + 2 + padding * 2;
            g.FillRectangle(brush, x, y, diagnosticSize.Width + padding * 2, diagnosticSize.Height + padding * 2);
            g.DrawString(diagnostic, font, Brushes.White, x + padding, y + padding);
        }
    }
}
