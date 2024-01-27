using System;
using System.Diagnostics;
using System.Media;
using NAudio;
using NAudio.Dsp;
using NAudio.Wave;

namespace Dictionary.OtherForms
{
    public partial class AlfabetForm : Form
    {
        //Массив с буквами.
        private string[] letters =
        {
            "А а",
            "Ә ә",
            "Б б",
            "В в",
            "Г г",
            "Д д",
            "Е е",
            "Ё ё",
            "Ж ж",
            "Җ җ",
            "З з",
            "И и",
            "Й й",
            "К к",
            "Л л",
            "М м",
            "Н н",
            "Ң ң",
            "О о",
            "Ө ө",
            "П п",
            "Р р",
            "С с",
            "Т т",
            "У у",
            "Ү ү",
            "Ф ф",
            "Х х",
            "Һ һ",
            "Ц ц",
            "Ч ч",
            "Ш ш",
            "Щ щ",
            "Ъ ъ",
            "Ы ы",
            "Ь ь",
            "Э э",
            "Ю ю",
            "Я я"
        };
        //Массив с кнопками.
        private Button[] buttons = new Button[39];
        //Объект для воспроизведения звука.
        private WaveOut waveOut = new WaveOut();
        //Перменная для чтения потока звука mp3.
        Mp3FileReader soundReader;
        //Проверка повторного нажатия на одну и ту же кнопку.
        private bool buttonIsClicked = false; 

        public AlfabetForm()
        {
            InitializeComponent();
        }

        private void AlfabetForm_Load(object sender, EventArgs e)
        {
            #region Добавленние кнопок в массив.

            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;
            buttons[9] = button10;
            buttons[10] = button11;
            buttons[11] = button12;
            buttons[12] = button13;
            buttons[13] = button14;
            buttons[14] = button15;
            buttons[15] = button16;
            buttons[16] = button17;
            buttons[17] = button18;
            buttons[18] = button19;
            buttons[19] = button20;
            buttons[20] = button21;
            buttons[21] = button22;
            buttons[22] = button23;
            buttons[23] = button24;
            buttons[24] = button25;
            buttons[25] = button26;
            buttons[26] = button27;
            buttons[27] = button28;
            buttons[28] = button29;
            buttons[29] = button30;
            buttons[30] = button31;
            buttons[31] = button32;
            buttons[32] = button33;
            buttons[33] = button34;
            buttons[34] = button35;
            buttons[35] = button36;
            buttons[36] = button37;
            buttons[37] = button38;
            buttons[38] = button39;

            #endregion
            //Добавление настроенных кнопок.
            LettersAdd();
        }

        private void LettersAdd()
        {
            for(int index = 0; index < 39; index++)
            {
                buttons[index].Text = letters[index];
                buttons[index].Font = new System.Drawing.Font("Segoe UI", 12f);
                buttons[index].FlatStyle = FlatStyle.Flat;
                buttons[index].FlatAppearance.BorderColor = Color.Black;
                buttons[index].FlatAppearance.BorderSize = 2;
                buttons[index].FlatAppearance.MouseOverBackColor = Color.White;
                buttons[index].TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        #region Произношение букв.

        //Буква Ә.
        private void button2_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\\Users\\79603\\OneDrive\\Рабочий стол\\Разработка ПО(Шарага)\\Dictionary\\Dictionary\\Sounds\\Произношение слов с использованием татарских букв — aa (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }
        //Буква Җ.
        private void button10_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\Users\79603\OneDrive\Рабочий стол\Разработка ПО(Шарага)\Dictionary\Dictionary\Sounds\Произношение слов с использованием татарских букв — j (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }

        //Буква Ң.
        private void button18_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\Users\79603\OneDrive\Рабочий стол\Разработка ПО(Шарага)\Dictionary\Dictionary\Sounds\Произношение слов с использованием татарских букв — nn (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }

        //Буква Ө.
        private void button20_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\Users\79603\OneDrive\Рабочий стол\Разработка ПО(Шарага)\Dictionary\Dictionary\Sounds\Произношение слов с использованием татарских букв — oo (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }

        //Буква Ү.
        private void button26_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\Users\79603\OneDrive\Рабочий стол\Разработка ПО(Шарага)\Dictionary\Dictionary\Sounds\Произношение слов с использованием татарских букв — y (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }

        //Буква Һ.
        private void button29_Click(object sender, EventArgs e)
        {
            //Если пользователь нажмет повторно на кнопку, то звук прекратиться, если даже звук будет из другой кнопки, то тоже будет завершен.
            if (buttonIsClicked)
            {
                soundReader.Dispose();
                waveOut.Stop();
                buttonIsClicked = false;
            }
            else
            {
                soundReader = new Mp3FileReader(@"C:\Users\79603\OneDrive\Рабочий стол\Разработка ПО(Шарага)\Dictionary\Dictionary\Sounds\Произношение слов с использованием татарских букв — h (www.lightaudio.ru).mp3");
                waveOut.Init(soundReader);
                waveOut.Play();
                buttonIsClicked = true;
            }
        }

        #endregion

        //Отключение воспроизведения.
        private void button40_Click(object sender, EventArgs e)
        {
            waveOut.Stop();
        }

        //Очищение потока и выкдючение звука.
        private void AlfabetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(soundReader != null)
            {
                soundReader.Dispose();
            }
            waveOut.Stop();
        }
    }
}
