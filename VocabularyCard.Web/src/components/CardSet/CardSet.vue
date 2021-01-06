<template>
  <router-link
    v-bind:to="{ name: 'CardSetCards', params: { cardSetId: cardSet.Id } }"
    tag="div"
    class="card col-md-6"
    style="width: 18rem;"
  >
    <!-- <div class="card" style="width: 18rem;"> -->
    <div class="card-body">
      <h5 class="card-title">{{ cardSet.DisplayName }}</h5>
      <p class="card-text">{{ cardSet.Description }}</p>
    </div>
    <!-- </div> -->
  </router-link>
</template>

<script>
export default {
  props: ["cardSet"],
  methods: {},
  created: function() {},
};
</script>
