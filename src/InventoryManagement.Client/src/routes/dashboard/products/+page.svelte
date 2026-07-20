<script>
	import { goto } from '$app/navigation';
	import * as Dialog from '$lib/components/ui/dialog';
	import ProductForm from '$lib/components/ProductForm.svelte';

	let { data } = $props();

	let page = $derived(data.page);
	let categories = $derived(data.categories);
	let suppliers = $derived(data.suppliers);

	let open = $state(false);
	let editingProduct = $state(null);

	function createProduct() {
		editingProduct = null;
		open = true;
	}

	function editProduct(product) {
		editingProduct = product;
		open = true;
	}

	function previousPage() {
		goto(`/dashboard/products?pageNumber=${page.pageNumber - 1}&pageSize=${page.pageSize}`);
	}

	function nextPage() {
		goto(`/dashboard/products?pageNumber=${page.pageNumber + 1}&pageSize=${page.pageSize}`);
	}
</script>

<div class="mb-6 flex items-center justify-between">
	<h1 class="text-2xl font-bold">Products</h1>

	<Dialog.Root bind:open>
		<button class="rounded bg-black px-4 py-2 text-white" onclick={createProduct}
			>Add product</button
		>

		<Dialog.Content class="max-h-[90vh] overflow-y-auto sm:max-w-3xl">
			<Dialog.Header>
				<Dialog.Title>Create Product</Dialog.Title>

				<Dialog.Description>Add a new product to your inventory.</Dialog.Description>
			</Dialog.Header>

			<ProductForm product={editingProduct} {categories} {suppliers} />
		</Dialog.Content>
	</Dialog.Root>
</div>

{#if page.data.length === 0}
	<p>No products yet.</p>
{:else}
	{#each page.data as p (p.id)}
		<h2>{p.name}</h2>
		<p>{p.sku}</p>
		<p>{p.description ?? 'No description'}</p>
		<p>${p.price}</p>
		<p>{p.stock}</p>
		<p>{p.minimumStock}</p>

		<button onclick={() => editProduct(p)}>Edit</button>
	{/each}
{/if}

<button onclick={previousPage} disabled={page.pageNumber === 1}> Previous </button>

<span>
	Page {page.pageNumber} of {page.totalPages}
</span>

<button onclick={nextPage} disabled={page.pageNumber === page.totalPages}> Next </button>
