<template>
  <div class="col">
    <router-link
      v-bind:to="{ name: 'CardSetCards', params: { cardSetId: cardSet.Id } }"
      tag="div"
      class="card text-center border-success"
    >
      <!-- <div class="card" style="width: 18rem;"> -->
      <h3 class="card-header">{{ cardSet.DisplayName }}</h3>
      <div class="card-body">
        <h5 class="card-title">{{ cardSet.DisplayName }}</h5>
        <p class="card-text">{{ cardSet.Description }}</p>
        <p class="card-text">
          <small class="text-muted">Last updated 3 mins ago</small>
        </p>
      </div>
      <!-- </div> -->
    </router-link>
  </div>
</template>

<script>
export default {
  props: ["cardSet"],
  methods: {},
  created: function() {},
};
</script>
