<script>
	import Package from '@lucide/svelte/icons/package';
	import Archive from '@lucide/svelte/icons/archive';
	import { Tag, TriangleAlert } from '@lucide/svelte';
	import { VanIcon } from '@lucide/svelte';
	import StatsCard from '$lib/components/StatsCard.svelte';

	let { data } = $props();
	let dashboard = $derived(data.dashboard);
</script>

<div class="mt-2 grid w-full grid-cols-[repeat(auto-fit,minmax(210px,1fr))] gap-4">
	<StatsCard
		title="Total Products"
		value={dashboard.totalProducts}
		icon={Package}
		iconClass="text-blue-500"
	/>

	<StatsCard
		title="Inventory Value"
		value={`$${dashboard.totalInventoryValue}`}
		icon={Archive}
		iconClass="text-emerald-500"
	/>

	<StatsCard
		title="Low Stock"
		value={dashboard.lowStockProducts.length}
		icon={TriangleAlert}
		iconClass="text-amber-500"
	/>

	<StatsCard
		title="Total Categories"
		value={dashboard.totalCategories}
		icon={Tag}
		iconClass="text-amber-500"
	/>

	<StatsCard
		title="Total Suppliers"
		value={dashboard.totalSuppliers}
		icon={VanIcon}
		iconClass="text-amber-500"
	/>
</div>
