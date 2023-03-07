using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

namespace GeniusGame
{
    public partial class Genius : Form
    {
        List<String> colorSeq = new List<String>();
        List<String> playerSeq = new List<String>();
        int listaIndex, pontos;
        const float brilho = 0.7f;
        string atualColor;
        string[] cores = { "R", "Y", "B", "G" };
        bool podeJogar;
        Random rdn = new Random();
        SoundPlayer[] audios = { new SoundPlayer(Properties.Resources.beep2),
            new SoundPlayer (Properties.Resources.beep3)};
        public Genius()
        {
            InitializeComponent();
        }
        private void Genius_Load(object sender, EventArgs e)
        {

        }
        private void MostrarCor(PictureBox pic, Color corClara, Color corEscura, SoundPlayer som)
        {
            pic.BackColor = corClara; som.Play(); Application.DoEvents();
            Thread.Sleep(850); pic.BackColor = corEscura;
        }
        private void ProcCor(string tagPic)
        {
            foreach (var corEncontrada in Controls.OfType<PictureBox>())
            {
                if (corEncontrada.Tag.ToString() == tagPic)
                {

                    string tag = corEncontrada.Tag.ToString();

                    Color piscar = ControlPaint.Light(corEncontrada.BackColor, brilho);
                    SoundPlayer som = tag == "R" ? audios[0] : (tag == "Y" ? audios[1] : (tag == "B" ? audios[2] : audios[3]));
                    MostrarCor(corEncontrada, piscar, corEncontrada.BackColor, som);
                }
            }
        }
        private void SortearCor()
        {
            atualColor = cores[rdn.Next(0, cores.Length)];
            colorSeq.Add(atualColor);

            foreach (var cor in colorSeq)
            {
                Application.DoEvents();
                Thread.Sleep(850);
                ProcCor(cor);
            }

            podeJogar = true;
        }

        private void Clique(Object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (podeJogar)
            {
                podeJogar = false;
                atualColor = pb.Tag.ToString();
                playerSeq.Add(atualColor);
                ProcCor(atualColor);

                if (playerSeq[listaIndex] == colorSeq[listaIndex])
                {
                    pontos++; lblPoints.Text = "Pontos: " + pontos.ToString(); listaIndex++; Checar();

                } else {

                    MessageBox.Show("Errou a sequência!! Game Over!");

                }
            }
        }
        private void Checar()
        {
            if (listaIndex == colorSeq.Count)
            {
                listaIndex = 0; playerSeq.Clear(); SortearCor();

            }else{

                podeJogar = true;
        }
    }

        private void Opcoes(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;

            switch (menu.Text){

                case "Iniciar" : pontos = 0; lblPoints.Text = "Pontos: "; playerSeq.Clear(); colorSeq.Clear();
                    podeJogar = false; listaIndex = 0; SortearCor(); break;

                case "Sobre": string info = "Fácil (unico level disponível no momento"; MessageBox.Show(info);
                    break;

                case "Sair": Application.Exit(); break;
            }

        }
}
}
        



