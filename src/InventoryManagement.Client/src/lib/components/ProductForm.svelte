<script>
	import { enhance } from '$app/forms';
	import * as Dialog from '$lib/components/ui/dialog';

	let isSubmitting = $state(false);

	let { product = null, categories = [], suppliers = [] } = $props();
</script>

<form
	class="flex flex-col gap-4"
	method="POST"
	use:enhance={() => {
		isSubmitting = true;
		return async ({ update }) => {
			await update();
			isSubmitting = false;
		};
	}}
>
	{#if product}
		<input type="hidden" name="id" value={product.id} />
	{/if}

	<div class="grid grid-cols-1 gap-4 md:grid-cols-2">
		<div>
			<label for="name" class="mb-1 block text-sm font-medium"> Name </label>

			<input
				id="name"
				value={product?.name ?? ''}
				name="name"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="sku" class="mb-1 block text-sm font-medium"> SKU </label>

			<input
				id="sku"
				value={product?.sku ?? ''}
				name="sku"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="price" class="mb-1 block text-sm font-medium"> Price </label>

			<input
				id="price"
				value={product?.price ?? 0}
				type="number"
				step="0.01"
				name="price"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="stock" class="mb-1 block text-sm font-medium"> Stock </label>

			<input
				id="stock"
				value={product?.stock ?? 0}
				type="number"
				name="stock"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="minimumStock" class="mb-1 block text-sm font-medium"> Minimum Stock </label>

			<input
				id="minimumStock"
				value={product?.minimumStock ?? 5}
				type="number"
				name="minimumStock"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="imgUrl" class="mb-1 block text-sm font-medium"> Image URL </label>

			<input
				id="imgUrl"
				value={product?.imgUrl ?? ''}
				name="imgUrl"
				class="w-full rounded-md border p-2"
			/>
		</div>

		<div>
			<label for="categoryId" class="mb-1 block text-sm font-medium"> Category </label>

			<select
				id="categoryId"
				value={product?.categoryId}
				name="categoryId"
				class="w-full rounded-md border p-2"
				required
			>
				<option value="">Select Category</option>

				{#each categories as category (category.id)}
					<option value={category.id}>
						{category.name}
					</option>
				{/each}
			</select>
		</div>

		<div>
			<label for="supplierId" class="mb-1 block text-sm font-medium">Supplier</label>

			<select
				id="supplierId"
				value={product?.supplierId ?? ''}
				name="supplierId"
				class="w-full rounded-md border p-2"
			>
				<option value="">No Supplier</option>

				{#each suppliers as supplier (supplier.id)}
					<option value={supplier.id}>
						{supplier.name}
					</option>
				{/each}
			</select>
		</div>
	</div>

	<div>
		<label for="description" class="mb-1 block text-sm font-medium">Description</label>

		<textarea id="description" name="description" rows="4" class="w-full rounded-md border p-2"
			>{product?.description ?? ''}</textarea
		>
	</div>

	<div class="flex justify-end gap-2 border-t pt-4">
		<Dialog.Close asChild>
			<button type="button" class="rounded-md border px-4 py-2"> Cancel </button>
		</Dialog.Close>

		<button
			type="submit"
			class="rounded-md bg-black px-4 py-2 text-white disabled:cursor-not-allowed disabled:opacity-50"
			disabled={isSubmitting}
		>
			{#if isSubmitting}
				{product ? 'Saving...' : 'Creating...'}
			{:else}
				{product ? 'Save Changes' : 'Create Product'}
			{/if}
		</button>
	</div>
</form>
