<script>
	let { title, value, icon: Icon, iconClass = 'text-gray-500' } = $props();
</script>

<div
	class="flex items-center justify-between rounded-xl border border-gray-200 bg-white p-4 shadow-sm transition hover:shadow-md"
>
	<div class="min-w-0">
		<p class="truncate text-sm text-gray-500">
			{title}
		</p>

		<h2 class="mt-1 text-2xl leading-none font-bold">
			{value}
		</h2>
	</div>

	<div class="shrink-0 rounded-lg bg-gray-100 p-2.5">
		<Icon class={`size-5 ${iconClass}`} />
	</div>
</div>
