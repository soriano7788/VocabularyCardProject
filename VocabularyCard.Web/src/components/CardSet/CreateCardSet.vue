<template>
  <div
    class="row text-start g-2 rounded shadow pt-4 bg-create-panel bg-gradient"
  >
    <!-- <div class="col-md-5 "> -->
    <div class="row mb-3">
      <div class="col-11"></div>
      <div class="col-1">
        <button
          type="button"
          class="btn-close btn-close-white"
          @click="closePanel"
        ></button>
      </div>
    </div>
    <div class="row mb-3">
      <label for="card-set-name" class="col-form-label col-md-4">
        單字集名稱
      </label>
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
        >說明
      </label>
      <div class="col-md-8">
        <textarea
          id="card-set-description"
          cols="20"
          rows="10"
          class="form-control"
        ></textarea>
      </div>
    </div>
    <div class="row mb-3">
      <div class="col-md-4"></div>
      <div class="col-md-8">
        <button
          type="button"
          class="btn btn-secondary"
          @click="submitNewCardSet"
        >
          送出
        </button>
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
      this.closePanel();
    },
    closePanel: function() {
      this.$emit("closePanel");
    },
  },
};
</script>
