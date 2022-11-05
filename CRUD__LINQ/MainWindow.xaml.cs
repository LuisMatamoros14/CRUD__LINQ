using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace CRUD__LINQ
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext DataContext;

        public MainWindow()
        {
            InitializeComponent();
            
            string Connection = ConfigurationManager.ConnectionStrings["CRUD__LINQ.Properties.Settings.CrudLinqSql"].ConnectionString;

            DataContext = new DataClasses1DataContext(Connection);

            //InsertarEmpresas();
            //InsertarEmpleados();
            //InsertarCargos();
            //AsociarCargoEmpleado();
            //ActualizarEmpleado();
            EliminarEmpleado();
        }

        public void InsertarEmpresas()
        {
            //DataContext.ExecuteCommand("delete from Empresas");
            
            Empresas empresa = new Empresas();

            empresa.Nombre = "Heluje";

            DataContext.Empresas.InsertOnSubmit(empresa);
            DataContext.SubmitChanges();

            Empresas empresa1 = new Empresas();

            empresa1.Nombre = "Sabritas";

            DataContext.Empresas.InsertOnSubmit(empresa1);

            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.Empresas;
        }

        public void InsertarEmpleados()
        {
            //DataContext.ExecuteCommand("delete from Empleados");

            Empresas empresa = DataContext.Empresas.First(em => em.Nombre.Equals("Heluje"));
            Empresas empresa1 = DataContext.Empresas.First(em => em.Nombre.Equals("Sabritas"));

            List<Empleados> ListaEmpleados = new List<Empleados>();  

            ListaEmpleados.Add(new Empleados { Nombre="Juan", Apellido="Diaz", EmpresaId=empresa.Id});
            ListaEmpleados.Add(new Empleados { Nombre = "Ana", Apellido = "Lopez", EmpresaId = empresa1.Id });

            DataContext.Empleados.InsertAllOnSubmit(ListaEmpleados);
            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.Empleados;
        }

        public void InsertarCargos()
        {
            DataContext.Cargos.InsertOnSubmit(new Cargos { Nombre = "Director/a" });
            DataContext.Cargos.InsertOnSubmit(new Cargos { Nombre = "Gerente" });
            DataContext.Cargos.InsertOnSubmit(new Cargos { Nombre = "Administrativo" });

            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.Cargos;

        }

        public void AsociarCargoEmpleado()
        {
            //Empleados Juan = DataContext.Empleados.First(em => em.Nombre.Equals("Juan"));
            //Empleados Ana = DataContext.Empleados.First(em => em.Nombre.Equals("Ana"));

            //Cargos CDirector = DataContext.Cargos.First(cargo => cargo.Nombre.Equals("Director/a"));
            //Cargos CGerente = DataContext.Cargos.First(cargo => cargo.Nombre.Equals("Gerente"));

            //CargosEmpelados CargoJuan = new CargosEmpelados();

            //CargoJuan.Empleados = Juan;
            //CargoJuan.CargoId = CDirector.Id;

            List<CargosEmpelados> ListaCargosEmpleados = new List<CargosEmpelados>();

            ListaCargosEmpleados.Add(new CargosEmpelados { Empleados = DataContext.Empleados.First(em => em.Nombre.Equals("Ana")), Cargos = DataContext.Cargos.First(cargo => cargo.Nombre.Equals("Director/a")) });
            

            DataContext.CargosEmpelados.InsertAllOnSubmit(ListaCargosEmpleados);
            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.CargosEmpelados;
        }

        public void ActualizarEmpleado()
        {
            Empleados empleado= DataContext.Empleados.First(em=>em.Nombre.Equals("Ana"));

            empleado.Nombre = "Ana Maria";

            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.Empleados;
        }

        public void EliminarEmpleado()
        {
            Empleados empleado = DataContext.Empleados.First(em => em.Nombre.Equals("Juan"));
            DataContext.Empleados.DeleteOnSubmit(empleado);
            DataContext.SubmitChanges();
            Principal.ItemsSource = DataContext.Empleados;
        }
    }
}
