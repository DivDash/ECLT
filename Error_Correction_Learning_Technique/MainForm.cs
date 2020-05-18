using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Error_Correction_Learning_Technique
{
    public partial class MainForm : MaterialForm
    {
        private int colorSchemeIndex;
        private readonly MaterialSkinManager materialSkinManager;
        private int[,] ANDPattern, ORPattern;
        private int[] ANDErrors, ANDdesiredY, ANDoutputY, ANDinputSample, ORErrors, ORdesiredY, ORoutputY, ORinputSample;
        private int varCount, ANDiterationCount, ORiterationCount = 0;
        private double[] ANDoldWeights, ANDnewWeights, ORoldWeights, ORnewWeights;
        public MainForm()
        {
            InitializeComponent();

            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;

            // Set this to false to disable backcolor enforcing on non-materialSkin components
            // This HAS to be set before the AddFormToManage()
            materialSkinManager.EnforceBackcolorOnAllComponents = true;

            // MaterialSkinManager properties
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);

        }

        private void ChangeColorButton_Click(object sender, EventArgs e)
        {
            colorSchemeIndex++;
            if (colorSchemeIndex > 17)
                colorSchemeIndex = 0;
            UpdateColor();
        }

        private void UpdateColor()
        {
            switch (colorSchemeIndex)
            {
                case 0:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal500 : Primary.Indigo500,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal700 : Primary.Indigo700,
                        materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? Primary.Teal200 : Primary.Indigo100,
                        Accent.Pink200,
                        TextShade.WHITE);
                    break;

                case 1:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Red600,
                        Primary.Red700,
                        Primary.Red200,
                        Accent.Red100,
                        TextShade.WHITE);
                    break;

                case 2:
                    materialSkinManager.ColorScheme = new ColorScheme(
                     Primary.Orange800,
                     Primary.Orange900,
                     Primary.Orange500,
                     Accent.Orange200,
                     TextShade.WHITE);

                    break;
                case 3:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.DeepOrange800,
                        Primary.DeepOrange900,
                        Primary.DeepOrange500,
                        Accent.DeepOrange200,
                        TextShade.WHITE);
                    break;
                case 4:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Yellow800,
                        Primary.Yellow900,
                        Primary.Yellow500,
                        Accent.Yellow200,
                        TextShade.WHITE);
                    break;
                case 5:
                    materialSkinManager.ColorScheme = new ColorScheme(
                          Primary.LightGreen600,
                          Primary.LightGreen700,
                          Primary.LightGreen200,
                          Accent.LightGreen100,
                          TextShade.WHITE);
                    break;
                case 6:
                    materialSkinManager.ColorScheme = new ColorScheme(
                          Primary.Green600,
                          Primary.Green700,
                          Primary.Green200,
                          Accent.Green100,
                          TextShade.WHITE);
                    break;
                case 7:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.BlueGrey800,
                        Primary.BlueGrey900,
                        Primary.BlueGrey500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
                case 8:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Blue800,
                        Primary.Blue900,
                        Primary.Blue500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
                case 9:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.LightBlue800,
                        Primary.LightBlue900,
                        Primary.LightBlue500,
                        Accent.LightBlue200,
                        TextShade.WHITE);
                    break;
                case 10:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Indigo800,
                        Primary.Indigo900,
                        Primary.Indigo500,
                        Accent.Indigo200,
                        TextShade.WHITE);
                    break;
                case 11:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Purple800,
                        Primary.Purple900,
                        Primary.Purple500,
                        Accent.Purple200,
                        TextShade.WHITE);
                    break;
                case 12:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.DeepPurple800,
                        Primary.DeepPurple900,
                        Primary.DeepPurple500,
                        Accent.DeepPurple200,
                        TextShade.WHITE);
                    break;
                case 13:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Brown800,
                        Primary.Brown900,
                        Primary.Brown500,
                        Accent.Pink200,
                        TextShade.WHITE);
                    break;
                case 14:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Lime800,
                        Primary.Lime900,
                        Primary.Lime500,
                        Accent.Lime200,
                        TextShade.WHITE);
                    break;
                case 15:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Teal800,
                        Primary.Teal900,
                        Primary.Teal500,
                        Accent.Teal200,
                        TextShade.WHITE);
                    break;
                case 16:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Amber800,
                        Primary.Amber900,
                        Primary.Amber500,
                        Accent.Amber200,
                        TextShade.WHITE);
                    break;
                case 17:
                    materialSkinManager.ColorScheme = new ColorScheme(
                        Primary.Cyan800,
                        Primary.Cyan900,
                        Primary.Cyan500,
                        Accent.Cyan200,
                        TextShade.WHITE);
                    break;
            }
            Invalidate();
        }

        private void ThemeChangerButton_Click(object sender, EventArgs e)
        {
            materialSkinManager.Theme = materialSkinManager.Theme == MaterialSkinManager.Themes.DARK ? MaterialSkinManager.Themes.LIGHT : MaterialSkinManager.Themes.DARK;

            switch (materialSkinManager.Theme)
            {
                case MaterialSkinManager.Themes.DARK:
                    ANDNeuralNetworkPicture.Image = Properties.Resources.NeuralNetworkWhite;
                    ANDLegendPicture.Image = Properties.Resources.Legend___White;

                    ORNeuralNetworkPicture.Image = Properties.Resources.NeuralNetworkWhite;
                    ORLegendPicture.Image = Properties.Resources.Legend___White;

                    NeuronActivityPicture.Image = Properties.Resources.NeuronActivityWhite;
                    HardLimitPicture.Image = Properties.Resources.HardLimitWhite;
                    MSEPicture.Image = Properties.Resources.MSE_White;
                    WeightAjusterPicture.Image = Properties.Resources.WeightAdjustmentWhite;
                    WeightUpdaterPicture.Image = Properties.Resources.WeightUpdaterWhite;

                    break;

                default:
                    ANDNeuralNetworkPicture.Image = Properties.Resources.NeuralNetwork;
                    ANDLegendPicture.Image = Properties.Resources.Legend;

                    NeuronActivityPicture.Image = Properties.Resources.NeuronActivity;
                    HardLimitPicture.Image = Properties.Resources.HardLimit;
                    MSEPicture.Image = Properties.Resources.MSE;
                    WeightAjusterPicture.Image = Properties.Resources.WeightAdjustment;
                    WeightUpdaterPicture.Image = Properties.Resources.WeightUpdater;

                    ORNeuralNetworkPicture.Image = Properties.Resources.NeuralNetwork;
                    ORLegendPicture.Image = Properties.Resources.Legend;

                    break;
            }

            UpdateColor();
        }

        private void UseColor_CheckedChanged(object sender, EventArgs e)
        {
            DrawerUseColors = UseColor.Checked;
        }
        private void HighlightAccent_CheckedChanged(object sender, EventArgs e)
        {
            DrawerHighlightWithAccent = HighlightAccent.Checked;
        }
        private void BackgroundAccent_CheckedChanged(object sender, EventArgs e)
        {
            DrawerBackgroundWithAccent = BackgroundAccent.Checked;
        }
        private void IconHider_CheckedChanged(object sender, EventArgs e)
        {
            DrawerShowIconsWhenHidden = IconHider.Checked;
        }
        private void t1Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[0, 0] = int.Parse(t1Box.Text);
        }
        private void t2Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[0, 1] = int.Parse(t2Box.Text);
        }
        private void t4Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[1, 0] = int.Parse(t4Box.Text);
        }
        private void t5Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[1, 1] = int.Parse(t5Box.Text);
        }
        private void t7Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[2, 0] = int.Parse(t7Box.Text);
        }
        private void t8Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[2, 1] = int.Parse(t8Box.Text);
        }
        private void t10Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[3, 0] = int.Parse(t10Box.Text);
        }
        private void t11Box_TextChanged(object sender, EventArgs e)
        {
            ANDPattern[3, 1] = int.Parse(t11Box.Text);
        }
        private void t3Box_TextChanged(object sender, EventArgs e)
        {
            ANDdesiredY[0] = int.Parse(t3Box.Text);
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ANDPattern = new int[4, 2] {
                         { int.Parse(t1Box.Text), int.Parse(t2Box.Text) } ,
                         { int.Parse(t4Box.Text), int.Parse(t5Box.Text) } ,
                         { int.Parse(t7Box.Text), int.Parse(t8Box.Text) },
                         { int.Parse(t10Box.Text),int.Parse(t11Box.Text)}
                    };
            ANDdesiredY = new int[4] { int.Parse(t3Box.Text), int.Parse(t6Box.Text), int.Parse(t9Box.Text), int.Parse(t12Box.Text) };

            ANDoldWeights = new double[2] { double.Parse(w1TextBox.Text), double.Parse(w2TextBox.Text) };
            ANDErrors = new int[4];
            ANDoutputY = new int[4];
            ANDinputSample = new int[2];

            ORPattern = new int[4, 2] {
                         { int.Parse(tBox1.Text), int.Parse(tBox2.Text) } ,
                         { int.Parse(tBox4.Text), int.Parse(tBox5.Text) } ,
                         { int.Parse(tBox7.Text), int.Parse(tBox8.Text) },
                         { int.Parse(tBox10.Text),int.Parse(tBox11.Text)}
                    };

            ORdesiredY = new int[4] { int.Parse(tBox3.Text), int.Parse(tBox6.Text), int.Parse(tBox9.Text), int.Parse(tBox12.Text) };
            ORoldWeights = new double[2] { double.Parse(ORw1Box.Text), double.Parse(ORw2Box.Text) };
            ORErrors = new int[4];
            ORoutputY = new int[4];
            ORinputSample = new int[2];
        }
        private void ANDResetButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        private void ANDw1Label_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("w1 = " + ANDw1Label.Text, ANDw1Label, 150, 0);
        }
        private void ANDw2Label_MouseEnter(object sender, EventArgs e)
        {
            toolTip2.Show("w2 = " + ANDw2Label.Text, ANDw2Label, 5, 100);
        }
        private void t6Box_TextChanged(object sender, EventArgs e)
        {
            ANDdesiredY[1] = int.Parse(t6Box.Text);
        }
        private void t9Box_TextChanged(object sender, EventArgs e)
        {
            ANDdesiredY[2] = int.Parse(t9Box.Text);
        }
        private void t12Box_TextChanged(object sender, EventArgs e)
        {
            ANDdesiredY[3] = int.Parse(t12Box.Text);
        }
        private void w1TextBox_TextChanged(object sender, EventArgs e)
        {
            ANDw1Label.Text = w1TextBox.Text;
        }
        private void w2TextBox_TextChanged(object sender, EventArgs e)
        {
            ANDw2Label.Text = w2TextBox.Text;
        }
        private void biasTextBox_TextChanged(object sender, EventArgs e)
        {
            ANDBiasLabel.Text = biasTextBox.Text;
        }
        private void eetaTextBox_TextChanged(object sender, EventArgs e)
        {
            eetaLabel.Text = eetaTextBox.Text;
        }
        private int HardLimitActivationFunction(double activityValue)
        {
            return activityValue < 0 ? 0 : 1;
        }
        private double NeuronActivity(int[,] pattern, double varBias, params double[] varWeights)
        {
            double varActivity = 0.0;

            for (int i = 0; i <= 1; i++)
            {
                varActivity += pattern[varCount, i] * varWeights[i];
            }

            varActivity += varBias;
            varCount++;
            return varActivity;
        }
        private int ErrorDetection(int desiredValue, int actualValue)
        {
            return desiredValue - actualValue;
        }
        private double[] AdjustWeight(double learningRate, double error, int[] sample)
        {
            double[] tempArray = new double[2] { 0, 0 };
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = learningRate * error * sample[i];
            }
            return tempArray;
        }
        private double MeanSquareError(int mean, params int[] error)
        {
            double varJ = 0.0;
            for (int i = 0; i < error.Length; i++)
            {
                varJ += error[i];
            }
            varJ = varJ / mean;
            return varJ;
        }
        private void ANDExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                ANDiterationCount++;
                Cursor = Cursors.WaitCursor;
                varCount = 0;

                ANDLogBox.AppendText("Executing the AND neural network...\n" +
                "\n---------------------------------------------------------------------" +
                "\t\t\tIteration #" + ANDiterationCount +
                "\n---------------------------------------------------------------------\n");
                activationLabel.Text = "Hard Limit";

                for (int i = 0; i < 4; i++)
                {
                    ANDoutputY[i] = HardLimitActivationFunction(
                                     NeuronActivity(
                                        ANDPattern,
                                        double.Parse(biasTextBox.Text),
                                        ANDoldWeights));

                    ANDLogBox.AppendText("\nOutput -> Yd" + (i + 1) + " = " + ANDdesiredY[i] + " ,  Ya" + (i + 1) + " = " + ANDoutputY[i]);

                    ANDErrors[i] = ErrorDetection(ANDdesiredY[i], ANDoutputY[i]);
                    ANDLogBox.AppendText("\nError -> e" + (i + 1) + " = " + ANDErrors[i] + "\n");
                }

                ANDLogBox.AppendText("\nCalculating Mean Square Error.....\n J " + " = " + MeanSquareError(4, ANDErrors) + "\n");

                for (int i = 0; i < 4; i++)
                {
                    if (ANDoutputY[i] != ANDdesiredY[i])
                    {
                        ANDLogBox.AppendText("\nOops! Ya" + (i + 1) + " does not match Yd" + (i + 1) + "!\n\nAjusting weights....");

                        for (int j = 0; j <= 1; j++)
                        {
                            ANDinputSample[j] = ANDPattern[i, j];
                        }

                        ANDnewWeights = AdjustWeight(double.Parse(eetaTextBox.Text), ANDErrors[i], ANDinputSample);

                        w1TextBox.Text = ANDnewWeights[0].ToString();
                        w2TextBox.Text = ANDnewWeights[1].ToString();

                        ANDLogBox.AppendText("\nΔw = [ " + ANDnewWeights[0] + " , " + ANDnewWeights[1] + "]\n\nUpdating weights....");

                        for (int k = 0; k < ANDoldWeights.Length; k++)
                        {
                            ANDoldWeights[k] = ANDoldWeights[k] + ANDnewWeights[k];
                        }

                        ANDuwLabel1.Text = ANDoldWeights[0].ToString();
                        ANDuwLabel2.Text = ANDoldWeights[1].ToString();

                        ANDw1Label.Text = ANDoldWeights[0].ToString();
                        ANDw2Label.Text = ANDoldWeights[1].ToString();

                        ANDLogBox.AppendText("\nΔw[n+1] = [ " + ANDoldWeights[0] + " , " + ANDoldWeights[1] + " ]\n\n");
                        ANDExecuteButton_Click(sender, e);
                    }
                }
                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(this, ex.Message);
            }
            ANDExecuteButton.Enabled = false;
        }
        private void ORExecuteButton_Click(object sender, EventArgs e)
        {
            try
            {
                ORiterationCount++;
                Cursor = Cursors.WaitCursor;
                varCount = 0;

                ORLogBox.AppendText("Executing the OR neural network...\n" +
                        "\n---------------------------------------------------------------------" +
                        "\t\t\tIteration #" + ORiterationCount +
                        "\n---------------------------------------------------------------------\n");
                ORactivationLabel.Text = "Hard Limit";

                for (int i = 0; i < 4; i++)
                {
                    ORoutputY[i] = HardLimitActivationFunction(
                                     NeuronActivity(
                                        ORPattern,
                                        double.Parse(ORBiasTextBox.Text),
                                        ORoldWeights));

                    ORLogBox.AppendText("\nOutput -> Yd" + (i + 1) + " = " + ORdesiredY[i] + " ,  Ya" + (i + 1) + " = " + ORoutputY[i]);

                    ORErrors[i] = ErrorDetection(ORdesiredY[i], ORoutputY[i]);
                    ORLogBox.AppendText("\nError -> e" + (i + 1) + " = " + ORErrors[i] + "\n");
                }

                ORLogBox.AppendText("\nCalculating Mean Square Error.....\n J " + " = " + MeanSquareError(4, ORErrors) + "\n");

                for (int i = 0; i < 4; i++)
                {
                    if (ORoutputY[i] != ORdesiredY[i])
                    {
                        ORLogBox.AppendText("\nOops! Ya" + (i + 1) + " does not match Yd" + (i + 1) + "!\n\nAjusting weights....");

                        for (int j = 0; j <= 1; j++)
                        {
                            ORinputSample[j] = ORPattern[i, j];
                        }

                        ORnewWeights = AdjustWeight(double.Parse(OReetaBox.Text), ORErrors[i], ORinputSample);

                        ORw1Box.Text = ORnewWeights[0].ToString();
                        ORw2Box.Text = ORnewWeights[1].ToString();

                        ORLogBox.AppendText("\nΔw = [ " + ORnewWeights[0] + " , " + ORnewWeights[1] + "]\n\nUpdating weights....");

                        for (int k = 0; k < ORoldWeights.Length; k++)
                        {
                            ORoldWeights[k] = ORoldWeights[k] + ORnewWeights[k];
                        }

                        ORuwLabel1.Text = ORoldWeights[0].ToString();
                        ORuwLabel2.Text = ORoldWeights[1].ToString();

                        ORw1Label.Text = ORoldWeights[0].ToString();
                        ORw2Label.Text = ORoldWeights[1].ToString();

                        ORLogBox.AppendText("\nΔw[n+1] = [ " + ORoldWeights[0] + " , " + ORoldWeights[1] + " ]\n\n");
                        ORExecuteButton_Click(sender, e);
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MaterialMessageBox.Show(this, ex.Message);
            }
            ORExecuteButton.Enabled = false;
        }
        private void tBox1_TextChanged(object sender, EventArgs e)
        {
            ORPattern[0, 0] = int.Parse(tBox1.Text);
        }
        private void tBox2_TextChanged(object sender, EventArgs e)
        {
            ORPattern[0, 1] = int.Parse(tBox2.Text);
        }
        private void tBox4_TextChanged(object sender, EventArgs e)
        {
            ORPattern[1, 0] = int.Parse(tBox4.Text);
        }
        private void tBox5_TextChanged(object sender, EventArgs e)
        {
            ORPattern[1, 1] = int.Parse(tBox5.Text);
        }
        private void ORw1Label_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("w1 = " + ORw1Label.Text, ORw1Label, 150, 0);
        }
        private void ORw2Label_MouseEnter(object sender, EventArgs e)
        {
            toolTip2.Show("w2 = " + ORw2Label.Text, ORw2Label, 5, 100);
        }

        private void materialTabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Tag)
            {
                case "0":
                    ResetText();
                    Text = "HOME";
                    break;

                case "1":
                    ResetText();
                    Text = "AND GATE";
                    break;

                case "2":
                    ResetText();
                    Text = "OR GATE";
                    break;

                case "3":
                    ResetText();
                    Text = "SETTINGS";
                    break;

                case "4":
                    ResetText();
                    Text = "ABOUT";
                    break;
            }
        }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://telicsolutions.com/");
        }

        private void LinkLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void LinkLabel_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void tBox7_TextChanged(object sender, EventArgs e)
        {
            ORPattern[2, 0] = int.Parse(tBox7.Text);
        }
        private void tBox8_TextChanged(object sender, EventArgs e)
        {
            ORPattern[2, 1] = int.Parse(tBox8.Text);
        }
        private void tBox10_TextChanged(object sender, EventArgs e)
        {
            ORPattern[3, 0] = int.Parse(tBox10.Text);
        }
        private void tBox11_TextChanged(object sender, EventArgs e)
        {
            ORPattern[3, 1] = int.Parse(tBox11.Text);
        }
        private void tBox3_TextChanged(object sender, EventArgs e)
        {
            ORdesiredY[0] = int.Parse(tBox3.Text);
        }
        private void tBox6_TextChanged(object sender, EventArgs e)
        {
            ORdesiredY[1] = int.Parse(tBox6.Text);
        }
        private void tBox9_TextChanged(object sender, EventArgs e)
        {
            ORdesiredY[2] = int.Parse(tBox9.Text);
        }
        private void tBox12_TextChanged(object sender, EventArgs e)
        {
            ORdesiredY[3] = int.Parse(tBox12.Text);
        }
        private void ORw1Box_TextChanged(object sender, EventArgs e)
        {
            ORw1Label.Text = ORw1Box.Text;
        }
        private void ORw2Box_TextChanged(object sender, EventArgs e)
        {
            ORw2Label.Text = ORw2Box.Text;
        }
        private void OReetaBox_TextChanged(object sender, EventArgs e)
        {
            OReetaLabel.Text = OReetaBox.Text;
        }
        private void ORBiasTextBox_TextChanged(object sender, EventArgs e)
        {
            ORBiasLabel.Text = ORBiasTextBox.Text;
        }
        private void ORResetButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}