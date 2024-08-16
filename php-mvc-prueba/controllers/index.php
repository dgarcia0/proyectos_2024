<?php
require_once("model/index.php");
class modeloControlador{

    private $model;

    function __construct()
    {
        $this->model = new Modelo();
    }

    //mostrar
    static function index(){
        $producto = new Modelo();
        $dato = $producto->mostrar("productos", "1");
        require_once("view/index.php");
    }
    //nuevo
    static function nuevo(){
        require_once("view/nuevo.php");
    }
    //guardar
    static function guardar(){
        $nombre = $_REQUEST['nombre'];
        $precio = $_REQUEST['precio'];
        $data = "'".$nombre."',".$precio;
        $producto = new Modelo();
        $dato = $producto->insertar("productos",$data);
        //redireccion
        header("location:".urlsite);
    }
    //editar
    static function editar(){
        $id = $_REQUEST['id'];
        $producto = new Modelo();
        $dato = $producto->mostrar("productos","id=".$id);
        require_once("view/editar.php");
    }
    //actualizar
    static function actualizar(){
        $id = $_REQUEST['id'];
        $nombre = $_REQUEST['nombre'];
        $precio = $_REQUEST['precio'];
        $data = "nombre='".$nombre."',precio=".$precio;
        $producto = new Modelo();
        $dato = $producto->actualizar("productos",$data, "id=".$id);
        //redireccion
        header("location:".urlsite);
    }
    //eliminar
    static function eliminar(){
        $id = $_REQUEST['id'];
        $producto = new Modelo();
        $dato = $producto->eliminar("productos","id=".$id);
        header("location:".urlsite);
    }
}