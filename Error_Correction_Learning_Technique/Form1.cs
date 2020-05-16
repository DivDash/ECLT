using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Windows.Forms;
namespace Error_Correction_Learning_Technique
{
    public partial class Form1 : MetroForm
    {
        private int[,] ANDPattern, ORPattern;
        private int[] ANDErrors, ANDdesiredY, ANDoutputY, ANDinputSample, ORErrors, ORdesiredY, ORoutputY, ORinputSample;
        private int varCount, ANDiterationCount, ORiterationCount = 0;
        private double[] ANDoldWeights, ANDnewWeights, ORoldWeights, ORnewWeights;

        public Form1()
        {
            InitializeComponent();
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
        private int HardLimitActivationFunction(double activityValue)
        {
            if (activityValue < 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private double[] adjustWeight(double learningRate, double error, int[] sample)
        {
            double[] tempArray = new double[2] { 0, 0 };
            for (int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = learningRate * error * sample[i];
            }
            return tempArray;
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Application.Restart();
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

        private void biasTextBox_TextChanged(object sender, EventArgs e)
        {
            biasLabel.Text = biasTextBox.Text;
        }

        private void eetaTextBox_TextChanged(object sender, EventArgs e)
        {
            eetaLabel.Text = eetaTextBox.Text;
        }

        private void ORexecuteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ORiterationCount++;
                Cursor = Cursors.WaitCursor;
                varCount = 0;
                ORLogBox.AppendText("Executing the OR neural network...\n" +
                "\n--------------------------------------------------------------------" +
                "-------------------------------------------------------------------------------------------------------------------------------" +
                "\n\t\t\t\t\t\t\t\t\t\tIteration # " + ORiterationCount +
                "\n---------------------------------------------------------------------------------------" +
                "------------------------------------------------------------------------------------------------------------\n");
                ORactivationLabel.Text = "Hard Limit";

                for (int i = 0; i < 4; i++)
                {
                    ORoutputY[i] = HardLimitActivationFunction(
                                     NeuronActivity(
                                        ORPattern,
                                        double.Parse(ORBiasBox.Text),
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

                        ORnewWeights = adjustWeight(double.Parse(OReetaBox.Text), ORErrors[i], ORinputSample);

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
                        ORexecuteBtn_Click(sender, e);
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message);
            }
            ORexecuteBtn.Enabled = false;
        }

        private void ANDexecuteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ANDiterationCount++;
                Cursor = Cursors.WaitCursor;
                varCount = 0;
                logBox.AppendText("Executing the AND neural network...\n" +
                "\n--------------------------------------------------------------------" +
                "-------------------------------------------------------------------------------------------------------------------------------" +
                "\n\t\t\t\t\t\t\t\t\t\tIteration # " + ANDiterationCount +
                "\n---------------------------------------------------------------------------------------" +
                "------------------------------------------------------------------------------------------------------------\n");
                activationLabel.Text = "Hard Limit";

                for (int i = 0; i < 4; i++)
                {
                    ANDoutputY[i] = HardLimitActivationFunction(
                                     NeuronActivity(
                                        ANDPattern,
                                        double.Parse(biasTextBox.Text),
                                        ANDoldWeights));

                    logBox.AppendText("\nOutput -> Yd" + (i + 1) + " = " + ANDdesiredY[i] + " ,  Ya" + (i + 1) + " = " + ANDoutputY[i]);

                    ANDErrors[i] = ErrorDetection(ANDdesiredY[i], ANDoutputY[i]);
                    logBox.AppendText("\nError -> e" + (i + 1) + " = " + ANDErrors[i] + "\n");
                }

                logBox.AppendText("\nCalculating Mean Square Error.....\n J " + " = " + MeanSquareError(4, ANDErrors) + "\n");

                for (int i = 0; i < 4; i++)
                {
                    if (ANDoutputY[i] != ANDdesiredY[i])
                    {
                        logBox.AppendText("\nOops! Ya" + (i + 1) + " does not match Yd" + (i + 1) + "!\n\nAjusting weights....");

                        for (int j = 0; j <= 1; j++)
                        {
                            ANDinputSample[j] = ANDPattern[i, j];
                        }

                        ANDnewWeights = adjustWeight(double.Parse(eetaTextBox.Text), ANDErrors[i], ANDinputSample);

                        w1TextBox.Text = ANDnewWeights[0].ToString();
                        w2TextBox.Text = ANDnewWeights[1].ToString();

                        logBox.AppendText("\nΔw = [ " + ANDnewWeights[0] + " , " + ANDnewWeights[1] + "]\n\nUpdating weights....");

                        for (int k = 0; k < ANDoldWeights.Length; k++)
                        {
                            ANDoldWeights[k] = ANDoldWeights[k] + ANDnewWeights[k];
                        }

                        ANDuwLabel1.Text = ANDoldWeights[0].ToString();
                        ANDuwLabel2.Text = ANDoldWeights[1].ToString();

                        ANDw1Label.Text = ANDoldWeights[0].ToString();
                        ANDw2Label.Text = ANDoldWeights[1].ToString();

                        logBox.AppendText("\nΔw[n+1] = [ " + ANDoldWeights[0] + " , " + ANDoldWeights[1] + " ]\n\n");
                        ANDexecuteBtn_Click(sender, e);
                    }
                }
                Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message);
            }
            ANDexecuteBtn.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
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
    }
}