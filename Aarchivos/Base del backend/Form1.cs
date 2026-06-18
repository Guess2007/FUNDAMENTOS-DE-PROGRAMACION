using Microsoft.Data.SqlClient; // Reemplaza System.Data.SqlClient por Microsoft.Data.SqlClient
using System;
using System.Data;

namespace Base_del_backend
{
    public partial class Form1 : Form
    {
        // IDE0044: Hacer el campo de solo lectura
        // CS0618: Usar Microsoft.Data.SqlClient en vez de System.Data.SqlClient
        // IDE0090: Simplificar la expresión "new"
        private readonly SqlConnection conn = new("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GenshinCalculator;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = "SELECT * FROM Personajes";
            cargarData(a);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string a = "SELECT * FROM Talentos";
            cargarData(a);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string a = "SELECT * FROM Armas";
            cargarData(a);
        }


        private void cargarData(string a)
        {
            SqlDataAdapter da = new(a, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.dataGridView1.DataSource = dt;
        }

        
    }
}
