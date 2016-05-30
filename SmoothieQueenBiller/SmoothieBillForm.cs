//***************************************************************************************************
// Program Name: Module 5: ABC Chiropractic
// Author:Samantha Cox
// Date: 4/30/2016
// Purpose: This program calculates the billing information for Smoothie Queen. It demonstrates 
//             use of multiple forms, including a splash screen that closes itself from within the 
//             'Program.cs file' and an about form that displays project information, use of 
//              funtions to return computed data, data parsing and manipulation from labels, text 
//              boxes, combo boxes, radio buttons, and check boxes. It is navigatable by a menu strip
//              and calculates the input amounts  using arrays and switches. Color and Font forms are
//              ,made available to for the amount due section of the program.
//***************************************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmoothieQueenBiller
{
    public partial class billingForm : Form
    {
        // create two dimensional array for smoothie cost
        decimal[,] smoothieListDecimal = {{5.99M, 6.99M, 7.99M, 8.99M },{ 6.99M, 7.99M, 8.99M, 9.99M }};
        //constants
        const decimal SALES_TAX_DEC = 0.08m;   //sales tax rate
        const decimal TOPPINGS_DEC = 0.75m;   //toppings cost
        const decimal PREF_DIS_DEC = 0.15m;   //preferred discount ammount 
        const decimal COUPON_DIS_DEC = 0.10m;   //coupon discount amount

        public billingForm()
        {
            InitializeComponent();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Make sure all fields are completed
            if (nameTextBox.Text == "" || addressTextBox.Text == "" || cityTextBox.Text == "" || stateTextBox.Text == "" || 
                zipTextBox.Text == "" || sizeComboBox.SelectedIndex < 0 || styleComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("All text fields, size, and style are required", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nameTextBox.Focus();
            }
            else 
            {
                decimal smoothieDec;        //amount for smoothie cost
                decimal toppingDec = 0m;    //amount for topping cost
                decimal discountDec = 0m;   //amount for discount cost
                decimal subtotalDec;        //cost after discount
                decimal taxDec;             //amount for tax cost
                decimal dueDec;             //amount for amount due cost
                int x=0, y=0;   //x= style selection, y= size selection
                //find style
                if (styleComboBox.SelectedIndex == 0)
                    x = 0;
                else
                    x = 1;
                //finde size
                switch(sizeComboBox.SelectedIndex)
                {
                    case 0:
                        {
                            y = 0;
                            break;
                        }
                    case 1:
                        {
                            y = 1;
                            break;
                        }
                    case 2:
                        {
                            y = 2;
                            break;
                        }
                    case 3:
                        {
                            y = 3;
                            break;
                        }
                }

                smoothieDec = smoothieListDecimal[x, y];

                // find topping cost
                if (echinaceaCheckBox.Checked == true)
                    toppingDec += TOPPINGS_DEC;
                if (pollenCheckBox.Checked == true)
                    toppingDec += TOPPINGS_DEC;
                if (energyCheckBox.Checked == true)
                    toppingDec += TOPPINGS_DEC;

                // find discount amount
                if (prefDisRadioButton.Checked == true)
                    discountDec = (smoothieDec + toppingDec) * PREF_DIS_DEC;
                else if (cDisRadioButton.Checked == true)
                    discountDec = (smoothieDec + toppingDec) * COUPON_DIS_DEC; 

                // find subtotal, tax, and total amount due
                subtotalDec = (smoothieDec + toppingDec) - discountDec;
                taxDec = findSalesTaxDecimal(subtotalDec);
                dueDec = subtotalDec + taxDec;

                //output all amounts
                smoothieTextBox.Text = smoothieDec.ToString("C");
                toppingTextBox.Text = toppingDec.ToString("C");
                discountTextBox.Text = discountDec.ToString("C");
                taxTextBox.Text = taxDec.ToString("C");
                dueTextBox.Text = dueDec.ToString("C");


            }
        }

        //clear all fields
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nameTextBox.Clear();
            addressTextBox.Clear();
            cityTextBox.Clear();
            stateTextBox.Clear();
            zipTextBox.Clear();
            sizeComboBox.SelectedIndex = -1;
            styleComboBox.SelectedIndex = -1;
            echinaceaCheckBox.Checked = false;
            pollenCheckBox.Checked = false;
            energyCheckBox.Checked = false;
            noDisRadioButton.Checked = true;
            smoothieTextBox.Clear();
            toppingTextBox.Clear();
            discountTextBox.Clear();
            taxTextBox.Clear();
            dueTextBox.Clear();
            nameTextBox.Focus();
            dueLabel.ForeColor = billingForm.DefaultForeColor;
            dueTextBox.Font = billingForm.DefaultFont;

        }

        //exit program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get current font
            fontDialog1.Font = dueTextBox.Font;
            //allow user to pick color
            fontDialog1.ShowDialog();
            //set font to user choice
            dueTextBox.Font = fontDialog1.Font;
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //get current font
            colorDialog1.Color = dueLabel.ForeColor;
            //allow user to pick color
            colorDialog1.ShowDialog();
            //set font to user choice
            dueLabel.ForeColor = colorDialog1.Color;
        }

        // display about form
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //declare new about form
            AboutBox1 aboutForm = new AboutBox1();
            //show form
            aboutForm.ShowDialog();
        }

        //function for finding tax
        private decimal findSalesTaxDecimal(decimal subtotalDec)
        {
            decimal taxDec;
            taxDec = subtotalDec * SALES_TAX_DEC;
            return (taxDec);
        }

    }
}
