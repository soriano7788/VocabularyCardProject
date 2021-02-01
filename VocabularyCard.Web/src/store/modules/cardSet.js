import axios from "axios";
import * as statusCode from "../consts/statusCode";
import router from "../../routes.js";

const state = {
    cardSets: [],
    currentCardSet: null,
    currentCardSetName: ""
};

const getters = {
    cardSets: state => {
        return state.cardSets;
    },
    currentCardSet: state => {
        return state.currentCardSet;
    },
    currentCardSetName: state => {
        return state.currentCardSetName;
    }
};

const mutations = {
    setCardSets: (state, payload) => {
        state.cardSets = payload;
    },
    setCurrentCardSet: (state, cardSet) => {
        state.currentCardSet = cardSet;
    },
    setCurrentCardSetName: (state, name) => {
        state.currentCardSetName = name;
    },
    appendNewCardSet: (state, newCardSet) => {
        state.cardSets.push(newCardSet);
    }
};

const actions = {
    fetchAllCardSets: ({ commit }) => {
        axios.get("cardset/getbyowner")
            .then(res => {
                console.log("cardset/getbyowner then:", res);

                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    const cardSets = result.data;
                    commit("setCardSets", cardSets);
                }
            }).catch(error => {
                console.log("cardset/getbyowner catch:", error);
            });
    },
    fetchCardSet: ({ commit }, cardSetId) => { },
    fetchCardSetName: ({ commit }, cardSetId) => {
        axios.get("cardset/GetName/" + cardSetId)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    commit("setCurrentCardSetName", result.data);
                }
            })
            .catch(error => {
                console.log(error);
            });
    },
    createCardSet: ({ commit }, cardSet) => {
        axios.post("cardset/Create", cardSet).then((res) => {
            const result = res.data;
            if (result.statusCode == statusCode.SUCCESS) {
                const newCardSet = result.data;
                commit("appendNewCardSet", newCardSet);
            }
        }).catch((error) => {
            console.log(error);
        });
    },
    removeCardSet: ({ commit }, cardSetId) => {
        axios.delete("cardset/delete/" + cardSetId)
            .then(res => {
                const result = res.data;
                if (result.statusCode == statusCode.SUCCESS) {
                    router.push("/CardSets");
                }
            })
            .catch(error => {
                console.log(error);
            });
    },
    clearCardSets: ({ commit }) => {
        commit("setCardSets", []);
    }
}

export default {
    state: state,
    getters: getters,
    mutations: mutations,
    actions: actions
};