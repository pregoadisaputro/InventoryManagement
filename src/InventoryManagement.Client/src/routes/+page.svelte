<script>
	import { goto } from '$app/navigation';
	let { data } = $props();
</script>

<h1>Inventory Management</h1>

{#if !data.user}
	<button onclick={() => goto('/auth/login')}>login</button>
{/if}
