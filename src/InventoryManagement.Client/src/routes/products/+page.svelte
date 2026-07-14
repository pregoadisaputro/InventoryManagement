<script>
	let { data } = $props();
	let page = $derived(data.page);

	function previousPage() {
		goto(`/products?pageNumber=${page.pageNumber - 1}&pageSize=${page.pageSize}`);
	}

	function nextPage() {
		goto(`/products?pageNumber=${page.pageNumber + 1}&pageSize=${page.pageSize}`);
	}
</script>

{#if page.data}
	{#each page.data as p (p.id)}
		<h1>{p.name}</h1>
		<p>SKU: {p.sku}</p>
		<p>Description: {p.description ?? 'No Description'}</p>
		<p>${p.price}</p>
		<p>Stock: {p.stock}</p>
		<p>Minimum Stock: {p.minimumStock}</p>
		<p>Img: {p.imgUrl ?? 'No image'}</p>
		<p>Category: {p.categoryName}</p>
		<p>Supplier {p.supplierName ?? 'No Supplier'}</p>
	{/each}
{/if}

<button onclick={previousPage} disabled={page.pageNumber === 1}>Previous</button>

<span>
	Page: {page.pageNumber} of {page.totalPages}
</span>

<button onclick={nextPage} disabled={page.pageNumber === page.totalPages}>Next</button>
