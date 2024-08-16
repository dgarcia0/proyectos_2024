<?php
require_once("config.php");
require_once("controllers/index.php");

if(isset($_GET['m'])):
    if(method_exists("modeloControlador",$_GET['m'])):
        modeloControlador::{$_GET['m']}();
    endif;
else:
    modeloControlador::index();
endif;
//phpinfo();
//var_dump(urlsite);