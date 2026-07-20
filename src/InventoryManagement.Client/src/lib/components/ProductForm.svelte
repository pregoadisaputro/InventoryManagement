<script>
	import { enhance } from '$app/forms';
	import * as Dialog from '$lib/components/ui/dialog';

	let { categories = [], suppliers = [] } = $props();
</script>

<form method="POST" use:enhance class="space-y-6">
	<div class="grid grid-cols-1 gap-4 md:grid-cols-2">
		<div>
			<label for="name" class="mb-1 block text-sm font-medium"> Name </label>

			<input id="name" name="name" class="w-full rounded-md border p-2" required />
		</div>

		<div>
			<label for="sku" class="mb-1 block text-sm font-medium"> SKU </label>

			<input id="sku" name="sku" class="w-full rounded-md border p-2" required />
		</div>

		<div>
			<label for="price" class="mb-1 block text-sm font-medium"> Price </label>

			<input
				id="price"
				type="number"
				step="0.01"
				name="price"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="stock" class="mb-1 block text-sm font-medium"> Stock </label>

			<input id="stock" type="number" name="stock" class="w-full rounded-md border p-2" required />
		</div>

		<div>
			<label for="minimumStock" class="mb-1 block text-sm font-medium"> Minimum Stock </label>

			<input
				id="minimumStock"
				type="number"
				name="minimumStock"
				class="w-full rounded-md border p-2"
				required
			/>
		</div>

		<div>
			<label for="imgUrl" class="mb-1 block text-sm font-medium"> Image URL </label>

			<input id="imgUrl" name="imgUrl" class="w-full rounded-md border p-2" />
		</div>

		<div>
			<label for="categoryId" class="mb-1 block text-sm font-medium"> Category </label>

			<select id="categoryId" name="categoryId" class="w-full rounded-md border p-2" required>
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

			<select id="supplierId" name="supplierId" class="w-full rounded-md border p-2">
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
		></textarea>
	</div>

	<div class="flex justify-end gap-2 border-t pt-4">
		<Dialog.Close asChild>
			<button type="button" class="rounded-md border px-4 py-2"> Cancel </button>
		</Dialog.Close>

		<button type="submit" class="rounded-md bg-black px-4 py-2 text-white">Create Product</button>
	</div>
</form>
