using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dictionary.OtherForms
{
    public partial class AboutProgram : Form
    {
        public AboutProgram()
        {
            InitializeComponent();
        }

        private void AboutProgram_Load(object sender, EventArgs e)
        {
            richTextBox1.ReadOnly = true;

            richTextBox1.Text = "Данная программа является интерактивным словарем для изучения татарского языка. Неважно какой у вас уровень знания татарского языка, данная программа расчитана на разный уровень знания. Вы можете изучить как слова, так алфавит, а с помощью игры в, которой нужно найти пары вы сможете попрактиковать свои занния. Когда вы выучите все доступные слова и узнаете о новых, вы можете добавить их в список словаря. Программа разработана студентом группы ИС-32 Каримовым Ильдаром";
        }
    }
}
