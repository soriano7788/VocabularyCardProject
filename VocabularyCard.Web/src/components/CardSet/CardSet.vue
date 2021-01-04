<template>
  <router-link
    to="/CardSets"
    tag="div"
    class="card col-md-6"
    style="width: 18rem;"
    @click="showAlert"
  >
    <!-- <div class="card" style="width: 18rem;"> -->
    <div class="card-body" @click="showAlert">
      <h5 class="card-title">{{ cardSet.DisplayName }}</h5>
      <p class="card-text">{{ cardSet.Description }}</p>
    </div>
    <!-- </div> -->
  </router-link>
</template>

<script>
export default {
  props: ["cardSet"],
  methods: {
    showAlert: function() {
      console.log(this.cardSet);
    },
  },
  created: function() {},
};
</script>
