using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace zach_var5_shvets
{
    public partial class Form1 :Form
    {
        List<string> mebel = new List<string>( );
        public Form1 ()
        {
            InitializeComponent( );
        }

        private void button1_Click (object sender, EventArgs e)
        {
            if ( CheckFile( ) )
            {
                ReadFromFile( );
                FillListBox( );                
            }
        }
        public void ReadFromFile ()
        {
            try
            {
                StreamReader sr = File.OpenText("mebel.txt");

                string [ ] array = sr.ReadLine( ).Split(' ');

                for ( int i = 0; i < array.Length; i++ )
                {
                    mebel.Add(array [ i ]);
                }
            }
            catch
            {
                MessageBox.Show("Файл заполнен неправильно");
            }
        }
        public bool CheckFile ()
        {
            if ( !File.Exists("mebel.txt") )
            {
                MessageBox.Show("Такого файла не существует");
                return false;
            }
            StreamReader sr = File.OpenText("mebel.txt");

            string str = sr.ReadToEnd( );

            if ( str.Length == 0 )
            {
                MessageBox.Show("Файл пуст");
                return false;
            }
            sr.Close( );

            return true;
        }
        public void FillListBox ()
        {
            button1.Enabled = false;

            listBox1.Items.Clear( );

            for(int i = 0; i<mebel.Count;i++ )
            {
                string str = mebel [i];
                listBox1.Items.Add($"{str}: {str [ 0 ]}");
            }
        }
        public bool CheckTextBox ()
        {
            if ( textBox1.Text.Length != 1 )
            {
                MessageBox.Show("Вы должны ввести только одну букву");
                return false;
            }
            char symbol = textBox1.Text [ 0 ];

            if ( !char.IsLetter(symbol) )
            {
                MessageBox.Show("Вы должны ввести букву");
                return false;
            }
            return true;
        }
        public void Delete ()
        {
            bool word_found = false;

            for (int i = 0; i<mebel.Count;i++ )
            {
                if (mebel[i].ToUpper( ).StartsWith(textBox1.Text.ToUpper( )) )
                {
                    mebel.RemoveAt(i);
                    word_found = true;
                    break;
                }
            }
            if ( word_found == true )
            {
                FillListBox( );
                MessageBox.Show("Слово было удалено");
            }
            else
            {
                MessageBox.Show($"Слово, которое начинается на букву {textBox1.Text} не было найдено");
            }
        }

        private void button2_Click (object sender, EventArgs e)
        {
            if ( mebel.Count != 0 )
            {
                if ( CheckTextBox( ) )
                {
                    Delete( );
                }
            }
            else
                MessageBox.Show("Вы сначала должны заполнить лист");
        }

        private void button3_Click (object sender, EventArgs e)
            {
            if ( mebel.Count != 0 )
            {
                if ( CheckWord( ) )
                {
                    AddWord( );
                    FillListBox( );
                }
            }
            else
            {
                MessageBox.Show("Сначала вы должны заполнить лист");
            }
        }
        public bool CheckWord ()
        {
            if ( textBox2.Text.Length == 0 )
            {
                MessageBox.Show("Вы не ввели слово");
                return false;
            }
            char symbol = textBox2.Text [ 0 ];
            if ( !char.IsLetter(symbol) )
            {
                MessageBox.Show("Слово должно начинаться с буквы");
                return false;
            }
            return true;

        }

        private void button4_Click (object sender, EventArgs e)
        {
            if ( mebel.Count != 0 )
            {
                var ordered = from meb in mebel
                              orderby meb
                              select meb;

                listBox1.Items.Clear( );

                foreach(var mebel in ordered )
                {
                    char first_letter = mebel [ 0 ];
                    listBox1.Items.Add($"{mebel}: {first_letter}");
                }
                MessageBox.Show("Слова были отсортированы по алфавиту");
            }
        }
        public void AddWord ()
        {
            bool word_exist = false;

            foreach(string str in mebel )
            {
                if(str.ToLower() == textBox2.Text.ToLower( ) )
                {
                    word_exist = true;
                    break;
                }
            }
            if(word_exist == true )
            {
                MessageBox.Show("Такое слово уже существует");
            }
            else
            {
                mebel.Add(textBox2.Text.ToLower( ));
                MessageBox.Show("Слово было добавлено");
            }                   
            
        }
            
    }
}
