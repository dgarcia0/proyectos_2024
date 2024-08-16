<?php
class Modelo{
    private $Modelo;
    private $db;
    private $datos;

    //constructor dinamico con variables construct($name, ...) y luego asignar a this las variables
    //se puede usar promoted properties para ahorrar codigo
    //readonly para no tocar las variables especificando tipo
    public function __construct()
    {
        $this->Modelo = array();
        $this->db = new PDO('mysql:host=localhost;dbname=mvc', "root", "");
    }

    //encriptamiento de contraseña
    //$pass = $_POST['password']
    //$password = hash('sha512', $pass)
    //insertar y select con contraseña encriptada

    //iniciar php session con session_start() al inicio
    //comprobar isset sesion para redirigir
    //al hacer logout session_destroy()

    //al hacer login crear sesion con correo usuario
    //$_SESSION['usuario'] = $email

    //fechas funcion date('Y-m-d', strtotime($_POST['fecha']))
    //fecha siendo input type date

    //revisar mysqli
    //mysqli_connect

    //especificar tipo de data
    //function get_data(string $url): array
    //pide string y devuelve array

    // $result = match($age){
    //     0,1,2 => "test",
    //     $age > 40 => "coso",
    //     default => ""
    // }

    //return new self para devolver propio objeto relleno

    public function insertar($tabla, $data){
        $consulta="insert into ".$tabla." values(null,". $data .")";
        $resultado=$this->db->query($consulta);
        if ($resultado) {
            return true;
        }else {
            return false;
        }
        
    }

    public function mostrar($tabla,$condicion){
        $consul="select * from ".$tabla." where ".$condicion.";";
        $resu=$this->db->query($consul);
        while($filas = $resu->FETCHALL(PDO::FETCH_ASSOC)) {
            $this->datos[]=$filas;
        }
        return $this->datos;
    }

    public function actualizar($tabla, $data, $condicion){       
        $consulta="update ".$tabla." set ". $data ." where ".$condicion;
        $resultado=$this->db->query($consulta);
        if ($resultado) {
            return true;
        }else {
            return false;
        }
    }

    public function eliminar($tabla, $condicion){
        $eli="delete from ".$tabla." where ".$condicion;
        $res=$this->db->query($eli);
        if ($res) {
            return true; 
        }else {
            return false;
        }
    }
}