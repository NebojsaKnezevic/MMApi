import { configureStore } from "@reduxjs/toolkit";
import surveySlice from "./survey/survey-slice";
import generalSlice from "./general/general-slice";
import modalSlice from "./login-modal/login-modal";

export const store = configureStore({
    reducer: {
        survey: surveySlice.reducer,
        general: generalSlice.reducer,
        loginModal: modalSlice.reducer
        
    }
});

export type RootState = ReturnType<typeof store.getState>;
// export type AppDispatch = typeof store.dispatch;