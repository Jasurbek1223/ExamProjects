<template>

  <!--Header / Navbar-->
  <header class="content-padding fixed gap-3 top-0 z-10 bg-white w-full h-20 px-20 flex items-center justify-between border-b border-gray-300">

    <!--Logo-->
    <div class="text-logoPrimary">
      <main-logo/>
    </div>

    <!--Locations menu-->
    <LocationsMenu/>

    <!--Profile-->
    <profile/>

    <!--Locations filter -->
    <locations-filter/>

  </header>

</template>

<script setup lang="ts">

import MainLogo from "@/common/components/icons/MainLogo.vue"
import LocationsMenu from "@/modules/locations/components/LocationsMenu.vue";
import Profile from "@/modules/Identity/components/Profile.vue";
import LocationsFilter from "@/modules/locations/components/LocationsFilter.vue";

</script>