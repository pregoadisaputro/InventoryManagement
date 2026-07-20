<script>
	import { goto } from '$app/navigation';
	import Button from '$lib/components/Button.svelte';
	import Card from '$lib/components/Card.svelte';

	let { data } = $props();
</script>

{#if !data.user}
	<div class="flex min-h-screen items-center justify-center">
		<Card title="Freego Inventory Management">
			<div class="flex flex-col gap-2">
				<Button title="Login" onclick={() => goto('/auth/login')} />
				<Button title="Register" onclick={() => goto('/auth/register')} />
			</div>
		</Card>
	</div>
{/if}
