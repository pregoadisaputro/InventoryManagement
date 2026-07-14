<script>
	import { goto } from '$app/navigation';

	let name = $state('');
	let sku = $state('');
	let description = $state('');
	let price = $state(0);
	let stock = $state(0);
	let minimumStock = $state(0);
	let imgUrl = $state('');
	let categoryId = $state(0);
	let supplierId = $state(0);

	let isSubmitting = $state(false);
	let errorMsg = $state('');

	async function handleCreateProduct() {
		errorMsg = '';
		isSubmitting = true;

		const response = await fetch('/products/new', {
			method: 'POST',
			headers: { 'Content-Type': 'application/json' },
			body: JSON.stringify({
				name,
				sku,
				description: description.trim() || null,
				price,
				stock,
				minimumStock,
				imgUrl: imgUrl.trim() || null,
				categoryId,
				supplierId: supplierId || null
			})
		});

		isSubmitting = false;

		if (!response.ok) {
			const errorData = await response.json();
			errorMsg = errorData.message || 'Failed to create product';
			return;
		}

		goto('/products');
	}
</script>

{#if errorMsg}
	<p>{errorMsg}</p>
{/if}

<input bind:value={name} placeholder="product name" />
<input bind:value={sku} placeholder="SKU" />
<input bind:value={description} placeholder="description" />
<input type="number" bind:value={price} placeholder="price" min="0" />
<input type="number" bind:value={stock} placeholder="stock" />
<input type="number" bind:value={minimumStock} placeholder="minimum stock" />
<input bind:value={imgUrl} placeholder="image url" />
<input type="number" bind:value={categoryId} placeholder="category id" />
<input type="number" bind:value={supplierId} placeholder="supplier id" />

<button onclick={handleCreateProduct} disabled={isSubmitting}>
	{isSubmitting ? 'Creating...' : 'Create Product'}
</button>
