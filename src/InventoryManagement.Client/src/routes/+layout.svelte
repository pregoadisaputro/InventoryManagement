<script>
	import './layout.css';

	let { data, children } = $props();
</script>

<nav>
	{#if data.user}
		<p>welcome, {data.user.username}</p>
	{/if}
</nav>

<main>{@render children()}</main>
