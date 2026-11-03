# 🛒 Point of Sale (POS) API - Sistema de Facturación

Una API RESTful robusta para la gestión de productos, facturación y control de inventario. Este sistema está diseñado con reglas de negocio estrictas para garantizar la integridad de las ventas y el historial de transacciones.

## 📖 Índice

* [Arquitectura y Decisiones de Diseño](#-arquitectura-y-decisiones-de-diseño)

* [Módulos de la API](#-módulos-de-la-api)

  * [📦 Módulo 1: Productos (Products)](#-módulo-1-productos-products)

  * [🧾 Módulo 2: Facturas (Receipt)](#-módulo-2-facturas-receipt)

  * [📋 Módulo 3: Detalle de Factura (DetailReceipt)](#-módulo-3-detalle-de-factura-detailreceipt)

* [⚙️ Reglas de Negocio](#️-reglas-de-negocio)

* [💾 Almacenamiento (CSV)](#-almacenamiento-csv)

## 🏛 Arquitectura y Decisiones de Diseño

> \[!IMPORTANT\]
> **La pregunta del millón:** *¿Qué pasa con una factura cerrada si mañana alguien cambia el precio o nombre del producto en el CSV?*
>
> **Respuesta del Diseño (Patrón Snapshot):** **Absolutamente nada.**
> Al momento de registrar una venta, la entidad `DetailReceipt` toma una "fotografía" (snapshot) de los datos del producto en ese instante exacto. Guarda una copia dura de la *descripción* y el *precio unitario*. De esta manera, el historial financiero de la factura queda sellado y es completamente independiente de las fluctuaciones futuras del catálogo de productos.

## 🚀 Módulos de la API

### 📦 Módulo 1: Productos (Products)

El catálogo es de solo lectura a nivel de endpoint. Los productos vienen precargados en un archivo CSV. El stock se actualiza **únicamente** a través del flujo de ventas (al agregar detalles a una factura).

**Entidad Mínima:** `Código`, `Nombre`, `Descripción`, `Precio`, `Stock`.

| Método | Endpoint | Descripción | 
| ----- | ----- | ----- | 
| `GET` | `/api/products` | Lista todo el catálogo de productos disponibles. | 
| `GET` | `/api/products/{id}` | Consulta la información detallada de un producto específico. | 

### 🧾 Módulo 2: Facturas (Receipt)

Maneja el ciclo de vida de la transacción. Una factura contiene: `Número`, `Fecha`, `Documento del Cliente`, `Nombre del Cliente`, `Estado` (Abierta/Cerrada) y `Total`.

> \[!NOTE\]
> El `Total` de la factura es de **solo lectura** para el cliente de la API; el backend lo calcula automáticamente sumando los subtotales de sus detalles.

| Método | Endpoint | Descripción | 
| ----- | ----- | ----- | 
| `GET` | `/api/receipts` | Lista todas las facturas en el sistema. | 
| `GET` | `/api/receipts/{id}` | Obtiene una factura específica junto con todos sus detalles (`DetailReceipt`). | 
| `POST` | `/api/receipts` | Abre una nueva factura (Estado inicial: Abierta). | 
| `POST` | `/api/receipts/{id}/details` | Agrega un producto (crea un detalle) a una factura abierta. | 
| `POST` | `/api/receipts/{id}/close` | Cierra la factura, bloqueándola para futuras modificaciones. | 

### 📋 Módulo 3: Detalle de Factura (DetailReceipt)

No expone endpoints propios. Funciona como entidad hija estricta de `Receipt`.

**Campos guardados (Snapshot de venta):**

* Descripción del producto (al momento de la venta).

* Precio unitario (al momento de la venta).

* Cantidad.

* Subtotal (Calculado).

## ⚙️ Reglas de Negocio

El sistema impone un control estricto sobre las operaciones para garantizar la consistencia de los datos:

1. **Gestión de Stock:**

   * 📉 Al agregar un detalle a la factura, se descuenta automáticamente el stock del producto en el CSV.

   * 🚫 **Validación de Inventario:** Si la cantidad solicitada supera el stock disponible, el detalle **no se agrega** y la API retorna un error (`400 Bad Request` o `422 Unprocessable Entity`).

2. **Cálculo de Totales:**

   * 🧮 El total de la factura es siempre la suma exacta de los subtotales de sus `DetailReceipt` hijos.

3. **Cierre de Facturas:**

   * ⚠️ **Prevención de facturas en cero:** Una factura sin detalles (vacía) **no se puede cerrar**.

   * 🔒 **Inmutabilidad:** Una vez que una factura pasa al estado **Cerrada**:

     * No se pueden agregar nuevos detalles.

     * No se pueden modificar los datos de la factura ni del cliente.

     * No se puede eliminar.

## 💾 Almacenamiento (CSV)

La persistencia de datos se maneja a través de archivos de texto plano (CSV).
Para mantener la integridad relacional en un entorno sin base de datos SQL:

* El CSV de `Products` se actualiza cada vez que se reduce el stock.

* Los detalles de los recibos se manejan serializando los datos, utilizando delimitadores secundarios (ej. `|` o `;`) si es necesario guardar relaciones 1:N en una sola línea, o manteniendo archivos separados (`receipts.csv` y `details.csv`) vinculados lógicamente por el `ReceiptId`.
## Arquitectura del Sistema
![Arquitectura del Sistema](./apistore.svg)
