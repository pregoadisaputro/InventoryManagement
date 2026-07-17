<script>
	let { title, value, icon: Icon, iconClass = 'text-gray-500' } = $props();
</script>

<div
	class="flex items-center justify-between rounded-xl border border-gray-200 bg-white p-5 shadow-sm transition hover:shadow-md"
>
	<div>
		<p class="text-sm text-gray-500">
			{title}
		</p>

		<h2 class="mt-1 text-4xl leading-none font-bold">
			{value}
		</h2>
	</div>

	<div class="rounded-xl bg-gray-100 p-3">
		<Icon class={`size-6 ${iconClass}`} />
	</div>
</div>
