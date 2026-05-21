SELECT * FROM agroglobal_system.productos;
select nombre_producto, categoria, precio_sugerido_exportacion 
from productos 
where sku like ('%1');
