<?php
require_once("layout/header.php");
?>
<a href="index.php?m=nuevo" class="btn">NUEVO</a>
<table>
    <tr>
        <td>ID</td>
        <td>NOMBRE</td>
        <td>ACCION</td>
    </tr>
    <tbody>
        <?php if(!empty($dato)):
            foreach($dato as $key => $value)
                foreach($value as $v):?>
                <tr>
                    <td><?= $v['id'] ?></td>
                    <td><?= $v['nombre'] ?></td>
                    <td>
                        <a class="btn" href="index.php?m=editar&id=<?= $v['id']?>">EDITAR</a>
                        <a class="btn" href="index.php?m=eliminar&id=<?= $v['id']?>" onclick="return confirm('seguro?'); false">ELIMINAR</a>
                    </td>
                </tr>
                <?php endforeach; ?>
        <?php else: ?>
            <tr>
                <td colspan="3">no hay registros</td>
            </tr>
        <?php endif ?>
    </tbody>
</table>
<?php
require_once("layout/foooter.php");