<script>
	let { data } = $props();
	let product = $derived(data.product);
</script>

<h1>{product.name}</h1>
<p>SKU: {product.sku}</p>
<p>Description: {product.description}</p>
<p>Price: ${product.price}</p>
<p>Stock: {product.stock}</p>
<p>Minimum Stock: {product.minimumStock}</p>
<p>Category: {product.categoryName}</p>
<p>Supplier: {product.supplierName ?? 'No supplier'}</p>
