﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopVk
{
    public partial class FormPhoto : Form
    {
        public FormPhoto(Bitmap bit)
        {
            InitializeComponent();
            pictureBox1.Image = bit;

        }
    }
}
