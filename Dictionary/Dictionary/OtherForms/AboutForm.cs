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
    public partial class AboutForm : Form
    {
        //Получение названия формы, о которой нужно получить информацию.
        private string selectedFormName = string.Empty;

        public AboutForm(string selectedFormName)
        {
            this.selectedFormName = selectedFormName;
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.Enabled = true;

            switch (selectedFormName)
            {
                case "search":
                    richTextBox1.Text = " Чтобы найти слово введите его в поле поиска и нажмите кнопку \"Найти\", если ваше слово присутсвует в словаре, то оно будет отмечено, если же его не будет в словаре, то выйдет соответсвующее сообщение." +
                        "\n" +
                        " Чтобы вернуться к первоначальному виду текстовых полей, нажмите на кнпку вправом верхнем углу";
                    break;
                case "gameAndAlfabet":
                    richTextBox1.Text = " Чтобы поиграть в игру \"Найди пару\" нажмите на кнопку \"Играть\"." + "\n" + " Для того, чтобы ознакомиться с алфавитом нажмите на кнопку \"Алфавит\".";
                    break;
                case "wordAdd":
                    //TODO: дописать описание.
                    richTextBox1.Text = " Чтобы добавить, удалить, обновить слово в базе данных нажмите на кнопку \"Добавление, удаление, обновление слов в базе\", будет открыта форма, для работы с которой сначала нужно будет выбрать категорию слова.";
                    break;
            }
        }
    }
}
