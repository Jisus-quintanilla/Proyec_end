using p1BC13Crud.Entidades;
using p1BC13Crud.Servicios;

namespace WFAlumnos
{
    public partial class Form1 : Form
    {
        ServicioAlumno srvAlumno = new();
        MdAlumno oAlumno = new();



        public Form1()
        {
            InitializeComponent();
        }


        void cargaGrid()
        {
            var resultado = srvAlumno.ConsultaSQL("select * from tb_alumnos");
            dataGridViewAlumno.DataSource = resultado;
        }

        private void button1Conexion_Click(object sender, EventArgs e)
        {
            cargaGrid();
        }

        
        void MapeoDatosFormalio (MdAlumno _alumnos)
        {
            
            textBoxCarnet.Text = _alumnos.carnet;
            textBoxNombre.Text = _alumnos.nombre;
            textBoxCorreo.Text = _alumnos.correo;
            comboBoxClase.Text = _alumnos.clase;
            comboBoxSeccion.Text = _alumnos.seccion;

            int parcial1 = _alumnos.parcial1;
            textBoxparcial1.Text = Convert.ToString(parcial1);
            int parcial2 = _alumnos.parcial2;
            textBoxparcial2.Text = Convert.ToString(parcial2);
            int parcial3 = _alumnos.parcial3;
            textBoxparcial3.Text = Convert.ToString(parcial3);
        }

   
        void buscaAlumno(string carnet)
        {
            oAlumno = null;
            oAlumno = srvAlumno.ObtenerAlumno(carnet);

            if (oAlumno == null)
            {
                MessageBox.Show("este cuate no esta");
                LimpiarFormulario();
            } else
            {
                MapeoDatosFormalio(oAlumno);
            }

        }


        private void buttonConsulta_Click(object sender, EventArgs e)
        {
            string carnet = textBoxCarnet.Text;
            buscaAlumno(carnet);
        }


        void LimpiarFormulario()
        {
            
            string dat = textBoxNombre.Text;
            
            oAlumno = new();
            MapeoDatosFormalio(oAlumno);
            // MapeoDatosFormalio(new());
            textBoxNombre.Text = dat;

        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            textBoxNombre.Text = " ";


        }


        private MdAlumno DatosForumulario()
        {
            MdAlumno _alumnos = new();
            _alumnos.carnet = textBoxCarnet.Text.Trim();
            _alumnos.nombre = textBoxNombre.Text.Trim();
            _alumnos.correo = textBoxCorreo.Text.Trim();
            _alumnos.clase = comboBoxClase.Text;
            _alumnos.seccion = comboBoxSeccion.Text;

            _alumnos.parcial1 = Convert.ToInt32(textBoxparcial1.Text);
            _alumnos.parcial2 = Convert.ToInt32(textBoxparcial2.Text);
            _alumnos.parcial3 = Convert.ToInt32(textBoxparcial3.Text);
            return _alumnos;
        }


        void info()
        {
            int info = 1;
            if (info > 0)
            {
                MessageBox.Show("Se a exterminado con exito");
                LimpiarFormulario();
                cargaGrid();
            }
            else
            {
                MessageBox.Show("Lo siento pero hay un error");
            }
        }

        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            oAlumno = DatosForumulario();
           int respusta = srvAlumno.crearAlumno(oAlumno);

            if (respusta > 0)
            {
                MessageBox.Show("Se grabo con exito");
                LimpiarFormulario();
                cargaGrid();
                
            } else
            {
                MessageBox.Show("Lo lamento hubo un clavo");
            }


        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            oAlumno = DatosForumulario();
            int respuesta = srvAlumno.actualizarAlumno(oAlumno);
            if (respuesta > 0)
            {
                MessageBox.Show("Se grabo con exito");
                LimpiarFormulario();
                cargaGrid();
            }
            else
            {
                MessageBox.Show("Lo lamento hubo un clavo");
            }

        }


        private void buttonImportar_Click(object sender, EventArgs e)
        {
            string archivo = @"C:\Datos visual\alunos.txt";
            ClsImportExport im = new();
            MessageBox.Show(im.importar(archivo));
        }

        private void buttonExportar_Click(object sender, EventArgs e)
        {
            string archivo = @"C:\Datos visual\DatoAlumno.txt";
            ClsImportExport im = new();
            MessageBox.Show(im.exportar("select * from tb_alumnos where seccion='B'", archivo));
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Queres eliminarlo?", "Desintegrar", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                oAlumno = DatosForumulario();
                int respuesta = srvAlumno.EliminarAlumno(oAlumno);
                info();
            }
            else if (dialogResult == DialogResult.No) ;

        }
    }
}