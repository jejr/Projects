using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HelpDesk.Grafico
{
    class Pie 
    {
        int _tamLeyenda = 10;
        int _espacioLeyenda = 5;
        string _campoGrupo = "";
        string _campoEvaluar = "";
        string _format = "g p c";

        public int Height { get; set; }
        public int Width { get; set; }
        public int Left { get; set; }
        public Color ForeColor { get; set; }
        public Font Font { get; set; }
        public string Format
        {
            get
            {
                if (this._format == null)
                    _format = "g p c";

                return _format;
            }
            set
            {
                if (value.Trim() != "")
                    _format = value.Trim().ToLower();
            }
        }

        public DataTable DataSource
        {
            get; set;
        }

        List<Color> colores = new List<Color>();
        DataTable tb = new DataTable();
        public Pie(string name)
        {
            this.Height = 100;
            this.Width = 100;
            this.Left = 0;
            this.ForeColor = Color.Black;
            this.Font = new Font("Arial",12);
        }

        public string CampoGrupo
        {
            get { return _campoGrupo; }
            set
            {

                if (this.DataSource.Columns[value] == null)
                    throw new ArgumentNullException("Nombre de campo no existe en el origen de datos");

                _campoGrupo = value;

            }
        }

        public string CampoEvaluar
        {
            get { return _campoEvaluar; }
            set
            {
                if (this.DataSource.Columns[value] == null)
                    throw new ArgumentNullException("Nombre de campo no existe en el origen de datos");

                _campoEvaluar = value;
            }
        }

        void LlenarColores()
        {
            this.colores.Clear();
            var pColores = typeof(Color).GetProperties();

            var lista = pColores.OrderByDescending(a => a.Name);

            foreach (var p in lista)
            {
                if (p.PropertyType == typeof(Color))
                {
                    Color c = (Color)p.GetValue(new Color(), null);

                    float sat = c.GetSaturation();
                    float brg = c.GetBrightness();

                    if (sat > 0.6F && brg <= 0.5F)
                        this.colores.Add(c);
                }

            }
        }

        public int EspacioLeyenda
        {
            get { return _espacioLeyenda; }
            set { _espacioLeyenda = value; }
        }
        public int TamLeyenda
        {
            get { return _tamLeyenda; }
            set { _tamLeyenda = value; }
        }

        public  void Dibujar(System.Drawing.Graphics g)
        {
            int top = 5;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                this.tb = this.DataSource.DefaultView.ToTable(true, this.CampoGrupo);
                tb.Columns.Add("Contador");
                tb.Columns.Add("Suma", typeof(int));
                tb.Columns.Add("Porc", typeof(decimal));
                tb.Columns.Add("Porciento");

                foreach (DataRow dr in tb.Rows)
                {
                    decimal contador = (int)this.DataSource.Compute(string.Format("Count({0})", this.CampoEvaluar), string.Format("{0} ='{1}' ", this.CampoGrupo, dr[this.CampoGrupo]));
                    int rows = this.DataSource.Rows.Count;
                    decimal porc = (contador / rows) * 100;
                    dr["Contador"] = contador;
                    // dr["Suma"] = this.Seccion.Document.DataSource.Compute(string.Format("Sum({0})", this.CampoEvaluar), string.Format("{0} ={1} ", this.CampoGrupo, dr[this.CampoGrupo]));
                    string dato = string.Format("{0:#0.00} %", porc);
                    dr["Porc"] = porc;
                    dr["Porciento"] = dato;
                }

                int leftCuadro = this.Left + Convert.ToInt32(this.Width * 0.8) + 5;
                Rectangle rec = new Rectangle(this.Left, top, Convert.ToInt32(this.Width * 0.8), this.Height);

                float anguloInicial = 0F;
                int topInicial = top;


                this.LlenarColores();
                foreach (DataRow dr in tb.Rows)
                {
                    float porc = Convert.ToSingle(dr["Porc"]);
                    float angulo = (360 * porc / 100);
                    Color C = this.colores.First();
                    this.colores.Remove(C);

                    SolidBrush sb = new SolidBrush(C);

                    g.FillPie(sb, rec, anguloInicial, angulo);
                    anguloInicial += angulo;

                    string result = this.Format.Replace("g", "{0}").Replace("p", "{1}").Replace("c", "{2}");
                    result = string.Format(result, dr[this.CampoGrupo].ToString(), dr["Porciento"].ToString(), dr["Contador"].ToString());
                    g.FillRectangle(sb, new Rectangle(leftCuadro, topInicial, this.TamLeyenda, this.TamLeyenda));
                    g.DrawString(result, this.Font, new SolidBrush(this.ForeColor), new PointF(leftCuadro + this.TamLeyenda + 5, topInicial - 5));


                    topInicial += (this.TamLeyenda + this.EspacioLeyenda);

                }

            }

        }
    }

