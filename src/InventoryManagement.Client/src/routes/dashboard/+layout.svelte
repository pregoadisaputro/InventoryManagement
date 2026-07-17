<script>
	import AppSidebar from '$lib/components/AppSidebar.svelte';
	import * as Sidebar from '$lib/components/ui/sidebar';

	let { children } = $props();
</script>

<Sidebar.Provider>
	<AppSidebar />

	<Sidebar.Inset>
		<main class="p-6">
			{@render children()}
		</main>
	</Sidebar.Inset>
</Sidebar.Provider>
