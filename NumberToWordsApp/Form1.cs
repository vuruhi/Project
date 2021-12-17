using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.IO.Compression;
using Word=Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace NumberToWordsApp
{
    public partial class Form1 : Form
    {
        private static string OutStr = "";//вывод строки
        private static string InFilename;
        private Word._Application wordObject;
        private Word.Document doc = null;
        private Word.Range range = null;
        //массивы с записью чисел
        private static string[] nums_1_9 = "ноль один два три четыре пять шесть семь восемь девять".Split();
        private static string[] nums_10_19 = "десять одиннадцать двенадцать тринадцать четырнадцать пятнадцать шестнадцать семнадцать восемнадцать девятнадцать".Split();
        private static string[] nums_20_90 = "ноль десять двадцать тридцать сорок пятьдесят шестьдесят семьдесят восемьдесят девяносто".Split();
        private static string[] nums_100_900 = "ноль сто двести триста четыреста пятьсот шестьсот семьсот восемьсот девятьсот".Split();
        private static string[] groupRzrd = @" тысяч миллион миллиард триллион квадриллион квинтиллион секстиллион септиллион октиллион нониллион дециллион андециллион дуодециллион тредециллион кваттордециллион квиндециллион сексдециллион септемдециллион октодециллион новемдециллион вигинтиллион анвигинтиллион дуовигинтиллион тревигинтиллион кватторвигинтиллион квинвигинтиллион сексвигинтиллион септемвигинтиллион октовигинтиллион новемвигинтиллион тригинтиллион антригинтиллион".Split();
        private static string[] nums_001 = "десят сот тысячн десятитысячн стотысячн миллионн десятимиллионн стомиллионн миллиардн десятимиллиардн стомиллиардн триллионн десятитрилионн стотриллионн".Split();

//блок обработки чисел
        //разбить на разряды (по три цифры)
        static IEnumerable<string> splitIntoCategories(string s)
        {
            //делим строку на части по три символа каждая, дополняем слева нулями, где необходимо
            if (s.Length % 3 != 0)
            {
                s = s.PadLeft(s.Length + 3 - s.Length % 3, '0');
            }
            //вывод результата (строка из трёх цифр)
            return Enumerable.Range(0, s.Length / 3).Select(i => s.Substring(i * 3, 3));
        }

        //реализует перевод числа в словесную форму
        static IEnumerable<string> Solve(IEnumerable<string> n)
        {
            var ii = 0;
            //идем по строке, представляющей число и выбираем запись числа по группам
            foreach (var s in n)
            {
                var countdown = n.Count() - ++ii;
                yield return
                    String.Format(@"{0} {1} {2} {3}",
                        s[0] == '0' ? "" : nums_100_900[getDigit(s[0])],
                        getE1(s[1], s[2]),
                        getE2(s[1], s[2], countdown),
                        s == "000" ? nums_1_9[0] : getRankName(s, countdown)
                    );
            }
        }

        //вторая цифра группы
        private static string getE1(char p1, char p2)
        {
            if (p1 != '0')
            {
                if (p1 == '1')//если на втором месте стоит 1, то выбираем слова из массива 10-19
                    return nums_10_19[getDigit(p2)];
                return nums_20_90[getDigit(p1)];//иначе смотрим десятки
            }
            return "";//если ноль, то ничего не пишем
        }

        //третья цифра группы
        private static string getE2(char p1, char p2, int cd)
        {
            if (p1 != '1')//если на втором месте не единица, то
            {
                if (p2 == '0') return "";//вместо нуля ничего не пишем
                //если 1, то пишем "одна" при группе тысяч, иначе обычное "один"
                if (p2 == '1') return (cd == 1 ? "одна" : nums_1_9[getDigit(p2)]);
                //если 2, то пишем "две" при группе тысяч, иначе обычное "два"
                return (p2 == '2' && cd == 1) ? "две" : nums_1_9[getDigit(p2)];
            }
            return "";//если на втором месте 1, то ничего не возвращаем, всё уже выбрано в getE1
        }

        private static int getDigit(char p1)
        //возвращает цифру, эквивалентную символу 
        {
            return Int32.Parse(p1.ToString());
        }

        //вывести название групп
        private static string getRankName(string s, int ii)
        {
            if (ii == 0) return "";//ноль не прописывается
            var r = groupRzrd[ii]; //выбираем из массива групп разрядов нужную
            //если последний символ строки 1, то добавляем "а" к тысяче и ничего не добавляем к миллиону
            if (s[2] == '1') return r + (ii == 1 ? "а" : "");
            //для 2-4 добавляем к окончанию тысяч "и", к другим разрядам "а"
            if (new[] { '2', '3', '4' }.Contains(s[2]))
            {
                return r + (ii == 1 ? "и" : "а");
            }
            else
                return r + (ii == 1 ? "" : "ов");//для остальных окончание "ов"
        }

//блок работы с документом и применения перевода цифровой формы в словесную
        public Form1()
        {
            InitializeComponent();
        }

        //перевод цифровой формы числа, введенного в текстовое, в словесную, вывод в другое текстовое поле
        private void Translatebtn_Click(object sender, EventArgs e)
        {
            OutStr = "";
            if (richTextBox1.SelectedText.Length != 0)
            {
                InNumbertxtBox.Text = richTextBox1.SelectedText.Trim();
                InNumbertxtBox.Text = InNumbertxtBox.Text.TrimEnd('\r', '\n');
            }
            var n = InNumbertxtBox.Text;
            if (n.Length > 0)
            {
                //входное число в строковой форме 
                //проверка на отрицательное число
                if (Convert.ToString(n[0]) == "-")
                {
                    OutStr = OutStr + "минус";
                    n = n.Substring(1);
                }
                //перевод в словесную форму
                foreach (var s in Solve(splitIntoCategories(n))) OutStr = OutStr + s + " ";
                // Вывод словесной формы;
                OutWordstxtBox.Text = OutStr.Trim();
            }
            else MessageBox.Show("Необходимо ввести число!");
        }

        //разбить строку на слова
        private static string[] SplitTextToWordsArray(string text)
        {
             return Regex.Split(text, @"\s+")
              .Where(word => word != "")
              .ToArray();
        }

        private void LoadDocx(string filename)
            //загрузка файла docx в richTextBox для проверки
        {
            wordObject = new Word.Application();
            object File = filename;
            object nullobject = System.Reflection.Missing.Value;
            //wordObject.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            Word._Document docs = null;
            try
            {
               docs = wordObject.Documents.Open(ref File, ref nullobject, 
                ref nullobject, ref nullobject, ref nullobject, ref nullobject, 
                ref nullobject, ref nullobject, ref nullobject, ref nullobject, 
                ref nullobject, ref nullobject, ref nullobject, ref nullobject, 
                ref nullobject, ref nullobject);
            docs.ActiveWindow.Selection.WholeStory();
            docs.ActiveWindow.Selection.Copy();
            richTextBox1.Paste();//вставка в ричтекстбокс загруженного документа
        } 
        catch (Exception ex)
            {
                //сообщение об ошибке 
                MessageBox.Show(ex.Message);
            }
            finally
            {
          //Очистка неуправляемых ресурсов, чтобы Word не подвисал в процессах
              if(docs != null)
                {
                    docs.Close(ref nullobject, ref nullobject, ref nullobject);
                }
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                    range = null;
                }
                if (wordObject != null)
                {
                    wordObject.Quit();
                    Marshal.ReleaseComObject(wordObject);
                    wordObject = null;
                }
            }
        }

         private void OpenDocxbtn_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            try
            {
                openFileDialog1.Filter = "doc files (*.doc,*.docx)|*.doc*";
                openFileDialog1.ShowDialog();
                InFilename = openFileDialog1.FileName;

                if (InFilename.Length > 0)
                {
                        LoadDocx(InFilename);
                }
            }
            catch (Exception ex)
            {
                //сообщение об ошибке 
                MessageBox.Show(ex.Message);
            }
        }

        //двойной щелчок по полю richTextBox1 закидывает в текстовое поле выделенный текст
        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int startIndex = richTextBox1.SelectionStart;
            int length = richTextBox1.SelectionLength;
            InNumbertxtBox.Text = richTextBox1.Text.Substring(startIndex, length).TrimEnd();
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            if (range != null)
            {
                Marshal.ReleaseComObject(range);
                range = null;
            }
            if (wordObject != null)
            {
                wordObject.Quit();
                Marshal.ReleaseComObject(wordObject);
                wordObject = null;
            }
            Close(); //выход из программы
        }

        private void Replacebtn_Click(object sender, EventArgs e)
        {
            string InStr = ""; OutStr = "";
            if (richTextBox1.SelectedText.Length != 0)
            //если есть выделенный фрагмент в ричтекстбоксе, то берем его
            {
                InStr = richTextBox1.SelectedText.Trim();
                InStr = InStr.TrimEnd('\r', '\n');
            }
            else
            {
                InStr = InNumbertxtBox.Text; //иначе то, что написано в текстовом поле
            }
            // var n = InNumbertxtBox.Text; //иначе то, что написано в текстовом поле
            if (InStr.Length > 0)
            {
                //входное число в строковой форме 
                if (Convert.ToString(InStr[0]) == "-")//проверяем на отрицательность
                {
                    OutStr = OutStr + "минус ";
                    InStr = InStr.Substring(1);
                }
                foreach (var s in Solve(splitIntoCategories(InStr))) OutStr = OutStr + s + " ";
                if (OutStr.Length > 0)
                {
                    wordObject = new Word.Application();
                    object File = InFilename;
                    object nullobject = System.Reflection.Missing.Value;
                    //wordObject.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                    Word._Document docs = null;
                    try
                    {
                        docs = wordObject.Documents.Open(ref File, ref nullobject,
                            ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                            ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                            ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                            ref nullobject, ref nullobject);

                        //Очищаем параметры поиска
                        wordObject.Selection.Find.ClearFormatting();
                        wordObject.Selection.Find.Replacement.ClearFormatting();
                        //Задаём параметры замены и выполняем замену.
                        object findText = InStr;
                        object replaceWith = OutStr.Trim();
                        object replace = 1;//однократная замена (для того, чтобы случайно не заменить часть большого числа
                        //считываем количество слов в документе
                        int count = docs.Words.Count;
                        bool f = true;//отвечает за наличие заменяемого слова
                        int i = 1;//счётчик слов в документе
                        // проходим по документу пословно
                        while (i<=count)
                        {
                            //ищем число по слову в документе, если находим - заменяем
                            if (docs.Words[i].Text.Trim() == InStr || docs.Words[i].Text.TrimEnd('\r', '\n') == InStr)
                            {
                                f = false;
                                Word.Range rng = docs.Words[i];
                                //выделяем найденное число и меняем его на словесную форму записи
                                rng.Select();
                                wordObject.Selection.Text = OutStr.Trim()+" ";
                                //обновляем текст в поле
                                richTextBox1.Clear();
                                docs.ActiveWindow.Selection.WholeStory();
                                docs.ActiveWindow.Selection.Copy();
                                richTextBox1.Paste();//вставка в ричтекстбокс загруженного документа
                                //считаем, на сколько слов увеличился документ
                                int k = 0;
                                string s = OutStr.Trim();
                                for (int j = 0; j <= s.Length - 1; j++)
                                {
                                    if (Convert.ToString(s[j]) == " ") k++;//нашли пробел - новое слово
                                }
                                count = count + k;//увеличиваем количество слов в документе
                            }
                            i++;//переходим к следующему слову
                        }
                        if (f) //сообщаем, если такого числа нет в документе
                        {
                            MessageBox.Show("Веденное число не найдено в документе");
                        }
                    }
                    catch (Exception ex)
                    {
                        //сообщение об ошибке 
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        /* Очистка неуправляемых ресурсов, чтобы Word не подвисал в процессах */
                        if (docs != null)
                        {
                            docs.Close(ref nullobject, ref nullobject, ref nullobject);
                        }
                        if (range != null)
                        {
                            Marshal.ReleaseComObject(range);
                            range = null;
                        }
                        if (wordObject != null)
                        {
                            wordObject.Quit();
                            Marshal.ReleaseComObject(wordObject);
                            wordObject = null;
                        }
                    }
                }
            }
        }

        private void ReplaceAllDocbtn_Click(object sender, EventArgs e)
        {
            wordObject = new Word.Application();
            object File = InFilename;
            object nullobject = System.Reflection.Missing.Value;
            string n;
            //wordObject.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
            Word._Document docs = null;
            try
            {
                docs = wordObject.Documents.Open(ref File, ref nullobject,
                 ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                 ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                 ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                 ref nullobject, ref nullobject);
                 //* Обработка основного текста документа*/
                 string InStr = "";
                string[] words= new string[0];
                 for (int i = 0; i < docs.Paragraphs.Count; i++)
                 {
                    if (words.Length > 0)
                    {
                        for (int j = 0; j < words.Length; j++)
                        { words[j] = ""; }
                    }
                    InStr = "";OutStr = "";//задаем пустые строки под поисковую строку и трасформированное число
                    //считываем абзац
                    //заполняем массив слов
                    words = SplitTextToWordsArray(" \r\n " + docs.Paragraphs[i + 1].Range.Text);
                    for (int j = 0; j < words.Length; j++)     
                    { 
                        InStr = words[j]; OutStr = "";
                        //проверяем текст это или число
                        if (Regex.IsMatch(InStr, @"-?\d+(\.\d+)?"))
                        { 
                            //Для обработки дробного числа 
                        }
                        //отсекаем знаки препинания после цифры
                        if (Convert.ToString(InStr[InStr.Length-1])=="." | Convert.ToString(InStr[InStr.Length - 1]) == ",")
                        {
                            words[j] = words[j].TrimEnd('.',',');
                            InStr = words[j];
                        }
                        //если слово является числом 
                        if (Regex.IsMatch(words[j], @"^\d+$"))
                        {
                            //преобразуем его в текстовую форму записи
                            n = words[j];
                            foreach (var s in Solve(splitIntoCategories(n))) OutStr = OutStr + s + " ";
                            //выделяем абзац, в котором встретили число
                            Word.Range rng = docs.Paragraphs[i + 1].Range;
                            //Очищаем параметры поиска
                            wordObject.Selection.Find.ClearFormatting();
                            wordObject.Selection.Find.Replacement.ClearFormatting();
                            //Задаём параметры замены и выполняем замену.
                            object findText = InStr;
                            object replaceWith = OutStr.Trim();
                            object replace = 1;
                            //производим замену
                            wordObject.Selection.Find.Execute(ref findText, ref nullobject, ref nullobject, ref nullobject,
                             ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref replaceWith,
                             ref replace, ref nullobject, ref nullobject, ref nullobject, ref nullobject);
                            //очищаем поле вывода и выводим обновленный документ
                            richTextBox1.Clear();
                            docs.ActiveWindow.Selection.WholeStory();
                            docs.ActiveWindow.Selection.Copy();
                            richTextBox1.Paste();//вставка в ричтекстбокс обновленного документа
                        }
                        else
                        {
                            if (Regex.IsMatch(words[j], @"^-\d+$"))
                            {
                                //проверка на отрицательное число
                                n = words[j];
                                if (Convert.ToString(n[0]) == "-")
                                {
                                    OutStr = OutStr + "минус ";
                                    n = n.Substring(1);
                                    foreach (var s in Solve(splitIntoCategories(n))) OutStr = OutStr + s + " ";
                                }
                                //выделяем абзац, в котором встретили число
                                Word.Range rng = docs.Paragraphs[i + 1].Range;
                                //Очищаем параметры поиска
                                wordObject.Selection.Find.ClearFormatting();
                                wordObject.Selection.Find.Replacement.ClearFormatting();
                                //Задаём параметры замены и выполняем замену.
                                object findText = InStr;
                                object replaceWith = OutStr.Trim();
                                object replace = 1;
                                //производим замену только в этом абзаце
                                wordObject.Selection.Find.Execute(ref findText, ref nullobject, ref nullobject, ref nullobject,
                                 ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref replaceWith,
                                 ref replace, ref nullobject, ref nullobject, ref nullobject, ref nullobject);
                                //очищаем поле вывода и выводим обновленный документ
                                richTextBox1.Clear();
                                docs.ActiveWindow.Selection.WholeStory();
                                docs.ActiveWindow.Selection.Copy();
                                richTextBox1.Paste();//вставка в ричтекстбокс обновленного документа
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //сообщение об ошибке 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                /* Очистка неуправляемых ресурсов, чтобы Word не подвисал в процессах */
                if (docs != null)
                {
                    docs.Close(ref nullobject, ref nullobject, ref nullobject);
                }
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                    range = null;
                }
                if (wordObject != null)
                {
                    wordObject.Quit();
                    Marshal.ReleaseComObject(wordObject);
                    wordObject = null;
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //завершение работы программы из меню Файл
            if (range != null)            //очистка мусора перед закрытием программы
            {
                Marshal.ReleaseComObject(range);
                range = null;
            }
            if (wordObject != null)
            {
                wordObject.Quit();
                Marshal.ReleaseComObject(wordObject);
                wordObject = null;
            }
            Close(); //выход из программы        
        }

            private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //завершение работы программы из меню Выход
            if (range != null)            //очистка мусора перед закрытием программы
                {
                Marshal.ReleaseComObject(range);
                range = null;
            }
            if (wordObject != null)
            {
                wordObject.Quit();
                Marshal.ReleaseComObject(wordObject);
                wordObject = null;
            }
            Close(); //выход из программы
        }

        private void открытьДокументToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocxbtn_Click(sender, e);
        }

        private void заменитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceAllDocbtn_Click(sender, e);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string hlpStr = "Программа предназначена для перевода целого числа"+"\n"+
                "в словесную форму. Программа обрабатывает как введенные " + "\n" +
                "с клавиатуры числа, так и документы Word." + "\n" +
                "Можно выбрать обработку всего документа, а можно заменить " +"\n" +
                "конкретное число, введенное в текстовое поле, в документе Word";
            MessageBox.Show(hlpStr);
        }
    }
}

    
