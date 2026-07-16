<script>
	import { Package } from '@lucide/svelte';

	let { title, description, children } = $props();
</script>

<div
	class="flex w-full max-w-sm flex-col gap-6 rounded-xl border border-gray-200 bg-white p-8 shadow"
>
	<header class="flex flex-col items-center gap-2 text-center">
		<Package size={28} />

		<h1 class="text-2xl font-semibold">{title}</h1>

		{#if description}
			<p class="text-sm text-gray-500">
				{description}
			</p>
		{/if}
	</header>

	<main>
		{@render children()}
	</main>
</div>
