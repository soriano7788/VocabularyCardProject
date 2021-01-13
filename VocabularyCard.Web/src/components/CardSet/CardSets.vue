<template>
  <div class="row justify-content-center">
    <!-- 這是 單字集 清單 -->
    <h1>CardSets List</h1>
    <div class="d-grid gap-2 d-md-block">
      <button
        type="button"
        class="btn btn-success btn-lg"
        @click="showCreatedForm = !showCreatedForm"
      >
        新增單字集
      </button>
    </div>

    <div class="row row-cols-1 row-cols-md-3 g-3 px-4">
      <card-set
        v-for="cardSet in cardSets"
        v-bind:key="cardSet.Id"
        v-bind:cardSet="cardSet"
      ></card-set>
    </div>

    <div class="col-md-5 position-fixed" v-if="showCreatedForm">
      <create-card-Set @closePanel="showCreatedForm = false"></create-card-Set>
    </div>
    <!-- 這個要用 slot 才行 -->
    <!-- <div v-for="cardSet in cardSets" :key="cardSet.Id">
      <h1>display name: {{ cardSet.DisplayName }}</h1>
    </div> -->
  </div>
</template>

<script>
import CardSet from "./CardSet.vue";
import CreateCardSet from "./CreateCardSet.vue";

export default {
  components: {
    cardSet: CardSet,
    createCardSet: CreateCardSet,
  },
  data: function() {
    return {
      showCreatedForm: false,
    };
  },
  computed: {
    cardSets: function() {
      return this.$store.getters.cardSets;
    },
  },
  methodds: {
    // getAllCardSets: function() {
    //   this.cardSets = this.$store.dispatch("getAllCardSets");
    // },
  },
  created: function() {
    this.$store.dispatch("fetchAllCardSets");
  },
  destroyed: function() {
    //clear cardSets
    this.$store.dispatch("clearCardSets");
  },
};
</script>
