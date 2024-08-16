<?php
require_once("layout/header.php");
?>
<h1 class="text-center">NUEVO</h1>
<form action="" method="get">
    <input type="text" placeholder="nombre" name="nombre"><br>
    <input type="text" placeholder="precio" name="precio"><br>
    <input type="submit" class="btn" name="btn" value="GUARDAR"><br>
    <input type="hidden" name="m" value="guardar">
</form>
<?php
require_once("layout/foooter.php");