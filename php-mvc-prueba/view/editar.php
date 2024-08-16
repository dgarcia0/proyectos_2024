<?php
require_once("layout/header.php");
?>
<h1 class="text-center">NUEVO</h1>
<form action="" method="get">
<?php
if(!empty($dato)):
    foreach($dato as $key => $value):
        foreach($value as $v):?>
        <input type="text" value="<?=$v['nombre']?>" name="nombre"><br>
        <input type="text" value="<?=$v['precio']?>" name="precio"><br>
        <input type="hidden" value="<?=$v['id']?>" name="id"><br>
        <input type="submit" class="btn" name="btn" value="ACTUALIZAR"><br>
        <input type="hidden" name="m" value="actualizar">
        <?php endforeach; 
    endforeach;
endif;?>
</form>
<?php
require_once("layout/foooter.php");