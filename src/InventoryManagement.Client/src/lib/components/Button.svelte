<script>
	let { title, ...props } = $props();
</script>

<button
	class="w-full cursor-pointer rounded-md bg-black px-4 py-2 text-white transition-colors hover:bg-gray-800 disabled:cursor-not-allowed disabled:bg-gray-400"
	{...props}>{title}</button
>
