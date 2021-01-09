<template>
  <div class="row  g-3">
    <!-- <div class="col-md-5 "> -->
    <div class="row mb-3">
      <label for="card-set-name" class="col-form-label col-md-4"
        >單字集名稱</label
      >
      <div class="col-md-8">
        <input
          type="text"
          v-model="displayName"
          class="form-control"
          id="card-set-name"
        />
      </div>
    </div>
    <div class="row mb-3">
      <label for="card-set-description" class="col-form-label col-md-4"
        >說明</label
      >
      <div class="col-md-8">
        <textarea
          id="card-set-description"
          cols="30"
          rows="10"
          class="form-control"
        ></textarea>
      </div>
    </div>
    <!-- </div> -->
  </div>
</template>

<script>
export default {
  data: function() {
    return {
      displayName: "",
      description: "",
    };
  },
  methods: {
    submitNewCardSet: function() {
      const cardSet = {
        displayName: this.displayName,
        description: this.description,
      };
      this.$store.dispatch("createCardSet", cardSet);
    },
  },
};
</script>
