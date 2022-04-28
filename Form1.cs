using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OKAH_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Инициализация всех таблиц
            DGV_Initialize();
            //Инициализация всех comboBox'ов
            CMB_Initialize();
            //ListBox'ов
            LB_Initialize();

            //Enter = Добавить деталь.
            this.AcceptButton = button1;

            //Скрыть textBox "Расстояние" со страницы 3
            textBox3.Visible = false;
            //Скрыть label "Расстояние" со страницы 3
            label10.Visible = false;
        }


        /*Список деталей, список сопряжений и спиок допусков*/
        static BindingList<Detail> details = new BindingList<Detail>();
        static BindingList<Link> links = new BindingList<Link>();
        static BindingList<Tolerance> tolerances = new BindingList<Tolerance>();

        //Базовая поверхность
        static Surface surface_base;


        //Настройка таблицы:
        void DGV_Initialize()
        {
            /*Таблица на странице "Детали"*/
            //checkBox-столбец
            DataGridViewCheckBoxColumn sel_1 = new DataGridViewCheckBoxColumn();
            {
                sel_1.HeaderText = "Выбрать";
                sel_1.Name = "Select";
                sel_1.FalseValue = false;
                sel_1.TrueValue = true;
            }
            dataGridView1.Columns.Add(sel_1);
            //Остальные:
            dataGridView1.DataSource = details;
            dataGridView1.Columns[1].HeaderText = "Id";
            dataGridView1.Columns[2].HeaderText = "Наименование детали";
            dataGridView1.Columns[3].HeaderText = "Число сопряжений";
            dataGridView1.Columns[4].HeaderText = "Число поверхностей";


            /*Таблица на странице "Сопряжения"*/
            //checkBox-столбец
            DataGridViewCheckBoxColumn sel_2 = new DataGridViewCheckBoxColumn();
            {
                sel_2.HeaderText = "Выбор";
                sel_2.Name = "Select";
                sel_2.FalseValue = false;
                sel_2.TrueValue = true;
            }
            dataGridView2.Columns.Add(sel_2);
            dataGridView2.DataSource = links;

            //Заголовки
            dataGridView2.Columns[0].HeaderText = "Выбор";
            dataGridView2.Columns[1].HeaderText = "Id";
            dataGridView2.Columns[2].HeaderText = "Первая деталь";
            dataGridView2.Columns[3].HeaderText = "Первая поверхность";
            dataGridView2.Columns[4].HeaderText = "Вторая деталь";
            dataGridView2.Columns[5].HeaderText = "Вторая поверхность";
            dataGridView2.Columns[6].HeaderText = "Тип сопряжения";


            /*Таблица на странице "Допуски"*/
            //comboBox-столбец для типов допусков
            dataGridView3.DataSource = tolerances;
            DataGridViewComboBoxColumn comBoxcol = new DataGridViewComboBoxColumn();

            List<KeyValuePair<Tolerance_Type, string>> combolist = new List<KeyValuePair<Tolerance_Type, string>>();
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Concentricity_tolerance, "Соосности"));
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Flatness_tolerance, "Плоскостности"));
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Perpendicularity_tolerance, "Перпендикулярности"));
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Positional_tolerance, "Позиционный"));
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Run_Out_tolerance, "Радиального биения"));
            combolist.Add(new KeyValuePair<Tolerance_Type, string>(Tolerance_Type.Tilt_tolerance, "Наклона"));

            comBoxcol.DataSource = combolist;
            comBoxcol.DisplayMember = "Value";
            comBoxcol.ValueMember = "Key";
            
            dataGridView3.Columns.Add(comBoxcol);
            dataGridView3.Columns[0].HeaderText = "Поверхность";
            dataGridView3.Columns[1].HeaderText = "Номинальное значение";
            dataGridView3.Columns[2].HeaderText = "Нижнее предельное значение";
            dataGridView3.Columns[3].HeaderText = "Верхнее предельное значение";
            dataGridView3.Columns[4].HeaderText = "Допуск";
            dataGridView3.Columns[5].HeaderText = "Тип допуска";
            dataGridView3.Columns[6].HeaderText = "Задать тип допуска";

            dataGridView3.Columns[4].ReadOnly = true;
        }



        //Настройка ComboBox:
        void CMB_Initialize()
        {
            //Первая деталь в сопряжении
            comboBox1.DisplayMember = "name";
            //comboBox1.ValueMember = "Id";
            comboBox1.DataSource = details;


            //Вторая деталь
            comboBox2.BindingContext = new BindingContext();
            comboBox2.DisplayMember = "name";
            //comboBox2.ValueMember = "Id";
            comboBox2.DataSource = details;


            //Тип сопряжения
            comboBox3.DisplayMember = "Value";
            comboBox3.ValueMember = "Key";

            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Tight_Contact, "Плотный контакт"));
            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Distance, "Расстояние"));
            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Gap_or_Conctact, "Зазор или контакт"));
            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Gap, "Зазор без контакта"));
            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Misalignment, "Cоосность"));
            comboBox3.Items.Add(new KeyValuePair<Link_Type, string>(Link_Type.Touch, "Касание"));


            //Тип поверхности
            comboBox6.DisplayMember = "Value";
            comboBox6.Items.Add(new KeyValuePair<Surface_Type, string>(Surface_Type.Cylinder, "Цилиндр"));
            comboBox6.Items.Add(new KeyValuePair<Surface_Type, string>(Surface_Type.Plane, "Плоскость"));


            //comboBox
            comboBox7.DisplayMember = "Value";
        }



        //Добавление элементов в таблицу:
        void DVG_Row_Add(string Name)
        {
            //////////////////////////////
            Detail det = new Detail(Name);

            //Пополняем список деталей
            details.Add(det);

            //Добавить treeView
            treeView1.Nodes.Add(Name);
        }



        //ListBox
        void LB_Initialize()
        {
            listBox1.DataSource = details;
            listBox1.DisplayMember = "name";

            listBox2.DataSource = details;
            listBox2.DisplayMember = "name";
        }




        /*КНОПКИ*/
        //Добавить деталь
        private void button1_Click(object sender, EventArgs e)
        {
            //Добавить в details
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                DVG_Row_Add(textBox1.Text);

                textBox1.Text = String.Empty;
            }
        }

        //Удалить деталь
        private void button2_Click(object sender, EventArgs e)
        {

            for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
            {
                if ((bool)dataGridView1.Rows[i].Cells[0].FormattedValue)
                {
                    //Удалить деталь из таблицы на странице 1:
                    details.RemoveAt(i);

                    //Удалить деталь из дерева на странице 2:
                    treeView1.Nodes.RemoveAt(i);
                }
            }

        }


        //Добавить сопряжение
        private void button3_Click(object sender, EventArgs e)
        {

            Detail det_1 = (Detail)comboBox1.SelectedItem;
            Detail det_2 = (Detail)comboBox2.SelectedItem;
            Link_Type lt = ((KeyValuePair<Link_Type, string>)comboBox3.SelectedItem).Key;
            Surface surf_1 = (Surface)comboBox4.SelectedItem;
            Surface surf_2 = (Surface)comboBox5.SelectedItem;
            //links.Add(new Link(det_1, det_2, lt));

            if (((KeyValuePair<Link_Type, string>)comboBox3.SelectedItem).Key == Link_Type.Distance)
            {
                //Добавить расстояние
                links.Add(new Link(surf_1, surf_2, lt, float.Parse(textBox3.Text.Replace(".", ",") )));
            }
            else
            {
                links.Add(new Link(surf_1, surf_2, lt));
            }
        }

        //Удалить сопряжение
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = dataGridView2.Rows.Count - 1; i >= 0; i--)
            {
                if ((bool)dataGridView2.Rows[i].Cells[0].FormattedValue)
                {
                    links.RemoveAt(i);
                }
            }
        }

        //Добавить поверхность
        private void button5_Click(object sender, EventArgs e)
        {
            //Добавить поверхность
            int index = listBox1.SelectedIndex;
            string Plane_name = textBox2.Text;
            Surface_Type type = ((KeyValuePair<Surface_Type, string>)comboBox6.SelectedItem).Key;
            details[index].add_surface(new Surface(Plane_name, type, details[index]));


            //Добавить в TreeView
            treeView1.Nodes[index].Nodes.Add(Plane_name+ " (" + ((KeyValuePair<Surface_Type, string>)comboBox6.SelectedItem).Value + ")");

            //Обновим comboBox7 на странице 5 (Геометрия)
            update_comboBox();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Detail det = (Detail)comboBox1.SelectedItem;
            comboBox4.DataSource = det.surfaces;
            comboBox4.DisplayMember = "name";
        }

        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Detail det = (Detail)comboBox2.SelectedItem;
            comboBox5.DataSource = det.surfaces;
            comboBox5.DisplayMember = "name";
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (((KeyValuePair<Link_Type, string>)comboBox3.SelectedItem).Key == Link_Type.Distance)
            {
                //Вывести элементы label и textBox
                textBox3.Visible = true;
                label10.Visible = true;
            }
            else
            {
                //Очистить
                textBox3.Clear();
                //Скрыть элементы label и textBox
                textBox3.Visible = false;
                label10.Visible = false;
            }
        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {

            if (!(listBox2.SelectedValue is null))
            {
                //Создаём новый список размеров 
                tolerances.Clear();
                foreach (Surface surf in ((Detail)listBox2.SelectedValue).surfaces)
                {
                    tolerances.Add(surf.tolerance);
                }
                dataGridView3.DataSource = tolerances;

                foreach (DataGridViewRow row in dataGridView3.Rows)
                {
                    row.Cells[0].Value = ((Detail)listBox2.SelectedValue).surfaces[row.Index].tolerance.tolerance_type;
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (!(row.Cells[0].Value is null))
                {
                    ((Detail)listBox2.SelectedItem).surfaces[row.Index].tolerance.tolerance_type = (Tolerance_Type)row.Cells[0].Value;
                    ((Detail)listBox2.SelectedItem).surfaces[row.Index].tolerance.tolerance = (float)row.Cells[4].Value - (float)row.Cells[3].Value;
                }
            }

            dataGridView3.Refresh();
        }

        //Обновить comboBox 
        void update_comboBox()
        {
            //Очистим comboBox
            comboBox7.Items.Clear();

            //Добавим поверхностей в comboBOx 
            foreach (Detail det in details)
            {
                
                foreach (Surface surf in det.surfaces)
                {
                    comboBox7.Items.Add( new KeyValuePair<Surface, string>(surf, $"{surf.name} ({det.name})") );
                }

            }

        }

    }

}