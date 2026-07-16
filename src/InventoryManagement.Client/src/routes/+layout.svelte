<script>
	let { data, children } = $props();
</script>

<header>
	{#if data.user}
		<p>welcome, {data.user.username}</p>
	{/if}
</header>

{@render children()}
