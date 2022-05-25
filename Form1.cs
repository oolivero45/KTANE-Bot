﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Timers;

namespace KTANE_Bot
{
    public partial class Form1 : Form
    {
        private KTANE_Speech _ktaneSpeech;
        public static string Output { get; set; }   
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize speech recognition.
            _ktaneSpeech = new KTANE_Speech();
            _ktaneSpeech.RecognitionEngine.SpeechRecognized += DefaultSpeechRecognized;
        }
        
        private void DefaultSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            textBoxInput.Text = e.Result.Text;
            textBoxOutput.Text = _ktaneSpeech.AnalyzeSpeech(e.Result.Text);
            _ktaneSpeech.Speak(textBoxOutput.Text);
        }
        

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //start listening.
            if (!_ktaneSpeech.Enabled)
            {
                _ktaneSpeech.Enable();
                buttonStart.Text = "STOP BOT";
                buttonStart.BackColor = Color.DarkRed;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
                
            }
            
            //stop listening.
            else
            {
                _ktaneSpeech.Disable();
                buttonStart.Text = "START BOT";
                buttonStart.BackColor = Color.Green;
                textBoxInput.Enabled = textBoxOutput.Enabled = _ktaneSpeech.Enabled;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            
        }
    }
}
