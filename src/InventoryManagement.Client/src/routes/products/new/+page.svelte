<script>
	import { enhance } from '$app/forms';

	let { form } = $props();
	let isSubmitting = $state(false);
</script>

<h1>Create New Product</h1>

{#if form?.errorMsg}
	<p style="color: red;">{form.errorMsg}</p>
{/if}

<form
	method="POST"
	use:enhance={() => {
		isSubmitting = true;
		return async ({ update }) => {
			isSubmitting = false;
			await update();
		};
	}}
>
	<input name="name" placeholder="product name" required />
	<input name="sku" placeholder="SKU" required />
	<input name="description" placeholder="description" />

	<input type="number" name="price" placeholder="price" min="0" step="0.01" required />
	<input type="number" name="stock" placeholder="stock" min="0" required />
	<input type="number" name="minimumStock" placeholder="minimum stock" min="0" required />

	<input name="imgUrl" placeholder="image url" />
	<input type="number" name="categoryId" placeholder="category id" required />
	<input type="number" name="supplierId" placeholder="supplier id (optional)" />

	<button type="submit" disabled={isSubmitting}>
		{isSubmitting ? 'Creating...' : 'Create Product'}
	</button>
</form>

<style>
	form {
		display: flex;
		flex-direction: column;
		gap: 0.75rem;
		max-width: 400px;
	}
</style>
