<template>

  <!--Main header-->
  <main-header/>

  <!--Content-->
  <article
      class="mt-[20px] bg-defaultBackground content-padding">
    <div class="flex flex-col items-center justify-center">
      <!--locations tab-->
      <locations-tab/>

      <!--Locations grid-->
      <locations-grid/>
    </div>
  </article>

</template>

<script setup lang="ts">

import LocationsTab from '@/modules/locations/components/LocationsTab.vue'
import LocationsGrid from '@/modules/locations/components/LocationsGrid.vue';
import MainHeader from '../components/MainHeader.vue';

</script>
