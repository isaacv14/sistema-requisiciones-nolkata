using System;
using System.Collections.Generic;
using System.Linq;

namespace Nolkata_Final
{
    // ==================== CLASE PERSONA (ABSTRACT - PADRE) ====================
    // Clase abstracta que sirve como base para UsuarioAdministrador y UsuarioBarco
    // Aplica el principio de herencia y reutilización de código
    public abstract class Persona
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idPersona;
        private string _nombre;
        private string _email;

        // ========== CONSTRUCTOR ==========
        // Constructor: inicializa los atributos con valores por defecto
        // Se ejecuta cuando se crea una instancia de una clase hija
        public Persona()
        {
            _idPersona = 0;
            _nombre = "";
            _email = "";
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdPersona() { return _idPersona; }
        public void SetIdPersona(long id) { _idPersona = id; }

        public string GetNombre() { return _nombre; }
        public void SetNombre(string nombre) { _nombre = nombre; }

        public string GetEmail() { return _email; }
        public void SetEmail(string email) { _email = email; }
        
        // ========== MÉTODO VIRTUAL ==========
        public virtual bool Autenticar(string contrasena) { return false; }
    }

    // ==================== CLASE USUARIOADMINISTRADOR (HIJA DE PERSONA) ====================
    // Clase hija de Persona. Representa al administrador del sistema.
    // Tiene permisos totales y puede crear/eliminar usuarios de barco.
    public class UsuarioAdministrador : Persona
    {
        private string _contrasena;
        private string _cargo;
        private bool _permisoTotal;

        // ========== CONSTRUCTOR ==========
        // Constructor: inicializa los atributos del administrador
        // El permisoTotal se establece en true por defecto
        public UsuarioAdministrador()
        {
            _contrasena = "";
            _cargo = "";
            _permisoTotal = true;
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public string GetContrasena() { return _contrasena; }
        public void SetContrasena(string contrasena) { _contrasena = contrasena; }

        public string GetCargo() { return _cargo; }
        public void SetCargo(string cargo) { _cargo = cargo; }

        public bool GetPermisoTotal() { return _permisoTotal; }
        public void SetPermisoTotal(bool permiso) { _permisoTotal = permiso; }

        // ========== MÉTODOS DE NEGOCIO ==========
        // Sobrescribe el método Autenticar de la clase Persona
        // Verifica si la contraseña ingresada coincide con la almacenada
        public override bool Autenticar(string contrasena)
        {
            return _contrasena == contrasena;
        }

        // Crea un nuevo usuario de barco en el sistema
        // Recibe un objeto UsuarioBarco con los datos del nuevo usuario
        public void CrearUsuarioBarco(UsuarioBarco nuevoUsuario)
        {
            Database.CrearUsuarioBarco(nuevoUsuario.GetNombre(), nuevoUsuario.GetContrasena());
        }

        // Elimina un usuario de barco por su ID
        // El historial se conserva en la tabla de auditoría
        public void EliminarUsuarioBarco(long idUsuario)
        {
            var usuarios = Database.GetUsuariosBarco();
            if (idUsuario >= 0 && idUsuario < usuarios.Count)
                Database.EliminarUsuarioBarco(usuarios[(int)idUsuario].Nombre);
        }
        
        // Lista todos los usuarios de barco registrados en el sistema
        public List<UsuarioBarco> ListarUsuariosBarco()
        {
            var lista = new List<UsuarioBarco>();
            foreach (var u in Database.GetUsuariosBarco())
            {
                var ub = new UsuarioBarco();
                ub.SetNombre(u.Nombre);
                ub.SetContrasena(u.Contrasena);
                ub.SetPermisoVerCosto(false);
                lista.Add(ub);
            }
            return lista;
        }
    }

    // ==================== CLASE USUARIOBARCO (HIJA DE PERSONA) ====================
    // Clase que representa a los usuarios de tipo barco (clientes marítimos)
    // Hereda de Persona y aplica el principio de herencia
    public class UsuarioBarco : Persona
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private string _contrasena;
        private long _idBarcoAsociado;
        private bool _permisoVerCosto;

        // ========== CONSTRUCTOR ==========
        // CONSTRUCTOR: Se ejecuta cuando se crea una nueva instancia de UsuarioBarco
        // Inicializa los atributos con valores por defecto
        public UsuarioBarco()
        {
            _contrasena = "";
            _idBarcoAsociado = 0;
            _permisoVerCosto = false;
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public string GetContrasena() { return _contrasena; }
        public void SetContrasena(string contrasena) { _contrasena = contrasena; }

        public long GetIdBarcoAsociado() { return _idBarcoAsociado; }
        public void SetIdBarcoAsociado(long id) { _idBarcoAsociado = id; }

        public bool GetPermisoVerCosto() { return _permisoVerCosto; }
        public void SetPermisoVerCosto(bool permiso) { _permisoVerCosto = permiso; }

        public override bool Autenticar(string contrasena)
        {
            return _contrasena == contrasena;
        }
    }

    // ==================== CLASE BARCO (INDEPENDIENTE) ====================
    // Clase que representa la información de un barco marítimo
    // Es independiente (no hereda de ninguna clase)
    public class Barco
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idBarco;
        private string _nombreBarco;
        private string _numeroMatricula;
        private string _contactoBarco;

        // ========== CONSTRUCTOR ==========
        // CONSTRUCTOR: Se ejecuta al crear un objeto Barco
        // Inicializa todos los atributos con valores por defecto
        public Barco()
        {
            _idBarco = 0;
            _nombreBarco = "";
            _numeroMatricula = "";
            _contactoBarco = "";
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdBarco() { return _idBarco; }
        public void SetIdBarco(long id) { _idBarco = id; }

        public string GetNombreBarco() { return _nombreBarco; }
        public void SetNombreBarco(string nombre) { _nombreBarco = nombre; }

        public string GetNumeroMatricula() { return _numeroMatricula; }
        public void SetNumeroMatricula(string matricula) { _numeroMatricula = matricula; }

        public string GetContactoBarco() { return _contactoBarco; }
        public void SetContactoBarco(string contacto) { _contactoBarco = contacto; }
    }

    // ==================== CLASE PRODUCTO (INDEPENDIENTE) ====================
    // Clase que representa los productos del inventario marítimo
    // Almacena información de repuestos, equipos, suministros, etc.
    public class Producto
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idProducto;
        private string _nombreProducto;
        private string _descripcionProducto;
        private int _stockDisponible;
        private decimal _precioReferencia;

        public Producto()
        {
            // ========== CONSTRUCTOR ==========
            // CONSTRUCTOR: Se ejecuta cuando se instancia un nuevo Producto
            // Establece valores iniciales para evitar null
            _idProducto = 0;
            _nombreProducto = "";
            _descripcionProducto = "";
            _stockDisponible = 0;
            _precioReferencia = 0;
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdProducto() { return _idProducto; }
        public void SetIdProducto(long id) { _idProducto = id; }

        public string GetNombreProducto() { return _nombreProducto; }
        public void SetNombreProducto(string nombre) { _nombreProducto = nombre; }

        public string GetDescripcionProducto() { return _descripcionProducto; }
        public void SetDescripcionProducto(string descripcion) { _descripcionProducto = descripcion; }

        public int GetStockDisponible() { return _stockDisponible; }
        public void SetStockDisponible(int stock) { _stockDisponible = stock; }

        public decimal GetPrecioReferencia() { return _precioReferencia; }
        public void SetPrecioReferencia(decimal precio) { _precioReferencia = precio; }
    }

    // ==================== CLASE REQUISICION (INDEPENDIENTE) ====================
    // Clase que representa una solicitud de productos (orden de compra)
    // Contiene el encabezado de la requisición (fecha, estado, total)
    public class Requisicion
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idRequisicion;
        private long _idBarcoSolicitante;
        private DateTime _fechaCreacion;
        private DateTime? _fechaCompletada;
        private decimal _costoTotal;
        private long _idUsuarioCreador;
        private List<DetalleRequisicion> _detalles;

        // ========== CONSTRUCTOR ==========
        // CONSTRUCTOR: Se ejecuta al crear una nueva requisición
        // La fecha se establece automáticamente con la fecha y hora actual
        public Requisicion()
        {
            _idRequisicion = 0;
            _idBarcoSolicitante = 0;
            _fechaCreacion = DateTime.Now;
            _fechaCompletada = null;
            _costoTotal = 0;
            _idUsuarioCreador = 0;
            _detalles = new List<DetalleRequisicion>();
        }


        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdRequisicion() { return _idRequisicion; }
        public void SetIdRequisicion(long id) { _idRequisicion = id; }

        public long GetIdBarcoSolicitante() { return _idBarcoSolicitante; }
        public void SetIdBarcoSolicitante(long id) { _idBarcoSolicitante = id; }

        public DateTime GetFechaCreacion() { return _fechaCreacion; }
        public void SetFechaCreacion(DateTime fecha) { _fechaCreacion = fecha; }

        public DateTime? GetFechaCompletada() { return _fechaCompletada; }
        public void SetFechaCompletada(DateTime? fecha) { _fechaCompletada = fecha; }

        public decimal GetCostoTotal() { return _costoTotal; }
        public void SetCostoTotal(decimal costo) { _costoTotal = costo; }

        public long GetIdUsuarioCreador() { return _idUsuarioCreador; }
        public void SetIdUsuarioCreador(long id) { _idUsuarioCreador = id; }

        public List<DetalleRequisicion> GetDetalles() { return _detalles; }
        public void SetDetalles(List<DetalleRequisicion> detalles) { _detalles = detalles; }

        public void AgregarDetalle(DetalleRequisicion detalle)
        {
            _detalles.Add(detalle);
            _costoTotal += detalle.GetCantidadSolicitada() * detalle.GetPrecioUnitario();
        }
    }

    // ==================== CLASE DETALLEREQUISICION (LA MÁS IMPORTANTE) ====================
    // CLASE PRINCIPAL: Representa cada línea de producto dentro de una requisición
    // Relaciona Requisicion con Producto (tabla intermedia o detalle)
    public enum EstadoProducto
    {
        Pendiente,
        EnProceso,
        Entregado
    }

    public class DetalleRequisicion
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idDetalle;
        private long _idRequisicionPadre;
        private long _idProductoSolicitado;
        private int _cantidadSolicitada;
        private EstadoProducto _estado;
        private DateTime _fechaUltimoCambio;
        private decimal _precioUnitario;
        private string _rutaImagen;

        // ========== CONSTRUCTOR ==========
        // CONSTRUCTOR: Se ejecuta al agregar un producto a una requisición
        // Calcula automáticamente el subtotal basado en cantidad y precio
        public DetalleRequisicion()
        {
            _idDetalle = 0;
            _idRequisicionPadre = 0;
            _idProductoSolicitado = 0;
            _cantidadSolicitada = 0;
            _estado = EstadoProducto.Pendiente;
            _fechaUltimoCambio = DateTime.Now;
            _precioUnitario = 0;
            _rutaImagen = "";
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdDetalle() { return _idDetalle; }
        public void SetIdDetalle(long id) { _idDetalle = id; }

        public long GetIdRequisicionPadre() { return _idRequisicionPadre; }
        public void SetIdRequisicionPadre(long id) { _idRequisicionPadre = id; }

        public long GetIdProductoSolicitado() { return _idProductoSolicitado; }
        public void SetIdProductoSolicitado(long id) { _idProductoSolicitado = id; }

        public int GetCantidadSolicitada() { return _cantidadSolicitada; }
        public void SetCantidadSolicitada(int cantidad) { _cantidadSolicitada = cantidad; }

        public EstadoProducto GetEstado() { return _estado; }
        public void SetEstado(EstadoProducto estado)
        {
            _estado = estado;
            _fechaUltimoCambio = DateTime.Now;
        }

        public DateTime GetFechaUltimoCambio() { return _fechaUltimoCambio; }
        public void SetFechaUltimoCambio(DateTime fecha) { _fechaUltimoCambio = fecha; }

        public decimal GetPrecioUnitario() { return _precioUnitario; }
        public void SetPrecioUnitario(decimal precio) { _precioUnitario = precio; }

        public string GetRutaImagen() { return _rutaImagen; }
        public void SetRutaImagen(string ruta) { _rutaImagen = ruta; }

        public void CambiarEstado(EstadoProducto nuevoEstado)
        {
            _estado = nuevoEstado;
            _fechaUltimoCambio = DateTime.Now;
        }

        public bool VerificarAtraso()
        {
            if (_estado == EstadoProducto.Pendiente)
            {
                return (DateTime.Now - _fechaUltimoCambio).Days > 3;
            }
            return false;
        }
    }

    // ==================== CLASE AUDITORIA (INDEPENDIENTE) ====================
    // Clase para registrar todas las acciones importantes del sistema
    // Útil para trazabilidad y seguridad
    public class Auditoria
    {
        // ========== ATRIBUTOS PRIVADOS (ENCAPSULAMIENTO) ==========
        private long _idAuditoria;
        private long _idUsuarioActor;
        private string _descripcionAccion;
        private string _nombreTablaAfectada;
        private DateTime _fechaHoraAccion;

        // ========== CONSTRUCTOR ==========
        // CONSTRUCTOR: Se ejecuta al crear un registro de auditoría
        // La fecha y hora se establecen automáticamente
        public Auditoria()
        {
            _idAuditoria = 0;
            _idUsuarioActor = 0;
            _descripcionAccion = "";
            _nombreTablaAfectada = "";
            _fechaHoraAccion = DateTime.Now;
        }

        // ========== GETTERS Y SETTERS (ENCAPSULAMIENTO) ==========
        public long GetIdAuditoria() { return _idAuditoria; }
        public void SetIdAuditoria(long id) { _idAuditoria = id; }

        public long GetIdUsuarioActor() { return _idUsuarioActor; }
        public void SetIdUsuarioActor(long id) { _idUsuarioActor = id; }

        public string GetDescripcionAccion() { return _descripcionAccion; }
        public void SetDescripcionAccion(string accion) { _descripcionAccion = accion; }

        public string GetNombreTablaAfectada() { return _nombreTablaAfectada; }
        public void SetNombreTablaAfectada(string tabla) { _nombreTablaAfectada = tabla; }

        public DateTime GetFechaHoraAccion() { return _fechaHoraAccion; }
        public void SetFechaHoraAccion(DateTime fecha) { _fechaHoraAccion = fecha; }

        public void RegistrarCambio()
        {
            Console.WriteLine($"Auditoría: {_descripcionAccion} - Usuario: {_idUsuarioActor}");
        }
    }

    // ==================== CLASES SIMPLES PARA ALMACENAMIENTO ====================
    public class UsuarioBasico
    {
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public bool EsAdmin { get; set; }
    }

    public class DetalleSimple
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCambio { get; set; }
    }

    public class RequisicionSimple
    {
        public int Id { get; set; }
        public string Barco { get; set; }
        public DateTime Fecha { get; set; }
        public string EstadoGeneral { get; set; }
        public decimal CostoTotal { get; set; }
        public string CreadoPor { get; set; }
        public List<DetalleSimple> Productos { get; set; }

        public RequisicionSimple()
        {
            Productos = new List<DetalleSimple>();
        }
    }

    public class ProductoSimple
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
    }

    // ==================== CLASE BARCODATA (ENCAPSULAMIENTO) ====================
    // Clase especializada para el manejo de datos de barcos
    // Demuestra encapsulamiento puro con atributos privados y métodos públicos
    public class BarcoData
    {
        private int _id;
        private string _nombre;
        private string _matricula;
        private string _contacto;

        public BarcoData(int id, string nombre, string matricula, string contacto)
        {
            _id = id;
            _nombre = nombre;
            _matricula = matricula;
            _contacto = contacto;
        }

        public int GetId() { return _id; }
        public void SetId(int id) { _id = id; }

        public string GetNombre() { return _nombre; }
        public void SetNombre(string nombre) { _nombre = nombre; }

        public string GetMatricula() { return _matricula; }
        public void SetMatricula(string matricula) { _matricula = matricula; }

        public string GetContacto() { return _contacto; }
        public void SetContacto(string contacto) { _contacto = contacto; }
    }

    // ==================== BASE DE DATOS ESTÁTICA ====================
    // Clase estática que simula una base de datos en memoria
// Almacena listas globales de usuarios, productos, barcos y requisiciones
    public static class Database
    {
        private static List<UsuarioBasico> _usuarios = new List<UsuarioBasico>();
        private static List<RequisicionSimple> _requisiciones = new List<RequisicionSimple>();
        private static List<ProductoSimple> _productos = new List<ProductoSimple>();
        private static List<BarcoData> _barcos = new List<BarcoData>();
        private static int _nextReqId = 1;
        private static int _nextProdId = 6;
        private static int _nextBarcoId = 1;

        static Database()
        {
            // Usuarios
            _usuarios.Add(new UsuarioBasico { Nombre = "admin", Contrasena = "1234", EsAdmin = true });
            _usuarios.Add(new UsuarioBasico { Nombre = "barco", Contrasena = "1234", EsAdmin = false });

            // Productos
            _productos.Add(new ProductoSimple { Id = 1, Nombre = "Filtro de aceite", Stock = 10, Precio = 15.00m });
            _productos.Add(new ProductoSimple { Id = 2, Nombre = "Disco de corte", Stock = 20, Precio = 4.50m });
            _productos.Add(new ProductoSimple { Id = 3, Nombre = "Brocha 2\"", Stock = 30, Precio = 3.00m });
            _productos.Add(new ProductoSimple { Id = 4, Nombre = "Aceite hidráulico", Stock = 5, Precio = 25.00m });
            _productos.Add(new ProductoSimple { Id = 5, Nombre = "Rodillo", Stock = 15, Precio = 8.00m });

            // Barcos
            _barcos.Add(new BarcoData(_nextBarcoId++, "Nolkata I", "IMO 1234567", "capitan@nolkata.com"));
            _barcos.Add(new BarcoData(_nextBarcoId++, "Nolkata II", "IMO 1234568", "capitan2@nolkata.com"));
            _barcos.Add(new BarcoData(_nextBarcoId++, "Nolkata III", "IMO 1234569", "capitan3@nolkata.com"));
        }

        // ========== USUARIOS ==========
        // Listas estáticas para almacenar usuarios (compartidas en toda la aplicación)
        public static UsuarioBasico ValidarLogin(string nombre, string contrasena)
        {
            return _usuarios.FirstOrDefault(u => u.Nombre == nombre && u.Contrasena == contrasena);
        }

        public static void CrearUsuarioBarco(string nombre, string contrasena)
        {
            if (!_usuarios.Any(u => u.Nombre == nombre))
                _usuarios.Add(new UsuarioBasico { Nombre = nombre, Contrasena = contrasena, EsAdmin = false });
        }

        public static void EliminarUsuarioBarco(string nombre)
        {
            var user = _usuarios.FirstOrDefault(u => u.Nombre == nombre && !u.EsAdmin);
            if (user != null)
                _usuarios.Remove(user);
        }

        public static List<UsuarioBasico> GetUsuariosBarco()
        {
            return _usuarios.Where(u => !u.EsAdmin).ToList();
        }

        // ========== REQUISICIONES ==========
        public static void CrearRequisicion(RequisicionSimple req)
        {
            req.Id = _nextReqId++;
            _requisiciones.Add(req);
        }

        public static List<RequisicionSimple> GetAllRequisiciones()
        {
            return _requisiciones;
        }

        public static void CambiarEstadoProducto(int reqId, int prodIndex, string nuevoEstado)
        {
            var req = _requisiciones.FirstOrDefault(r => r.Id == reqId);
            if (req != null && prodIndex < req.Productos.Count)
            {
                req.Productos[prodIndex].Estado = nuevoEstado;
                req.Productos[prodIndex].FechaCambio = DateTime.Now;

                bool todosEntregados = req.Productos.All(p => p.Estado == "Entregado");
                req.EstadoGeneral = todosEntregados ? "Entregado" : "En Proceso";
            }
        }

        // ========== PRODUCTOS ==========
        public static List<ProductoSimple> GetAllProductos()
        {
            return _productos;
        }

        public static void AgregarProducto(string nombre, int stock, decimal precio)
        {
            _productos.Add(new ProductoSimple { Id = _nextProdId++, Nombre = nombre, Stock = stock, Precio = precio });
        }

        public static void EditarProducto(int id, string nombre, int stock, decimal precio)
        {
            var prod = _productos.FirstOrDefault(p => p.Id == id);
            if (prod != null)
            {
                prod.Nombre = nombre;
                prod.Stock = stock;
                prod.Precio = precio;
            }
        }

        public static void EliminarProducto(int id)
        {
            var prod = _productos.FirstOrDefault(p => p.Id == id);
            if (prod != null)
                _productos.Remove(prod);
        }

        // ========== BARCOS ==========
        public static List<BarcoData> GetAllBarcos()
        {
            return _barcos;
        }

        public static void AgregarBarco(string nombre, string matricula, string contacto)
        {
            _barcos.Add(new BarcoData(_nextBarcoId++, nombre, matricula, contacto));
        }

        public static void EditarBarco(int id, string nombre, string matricula, string contacto)
        {
            var barco = _barcos.FirstOrDefault(b => b.GetId() == id);
            if (barco != null)
            {
                barco.SetNombre(nombre);
                barco.SetMatricula(matricula);
                barco.SetContacto(contacto);
            }
        }

        public static void EliminarBarco(int id)
        {
            var barco = _barcos.FirstOrDefault(b => b.GetId() == id);
            if (barco != null)
                _barcos.Remove(barco);
        }
    }
}

