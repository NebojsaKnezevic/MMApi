import { createSlice, PayloadAction } from "@reduxjs/toolkit"


export interface IBrowserSize{
    browserWidth?: number,
    browserHeight?: number
}

const initialState: IBrowserSize = {
    browserWidth: window.innerWidth,
    browserHeight: window.innerHeight,
}

const generalSlice = createSlice({
    name: 'general',
    initialState,
    reducers:{
        setBrowserSize: (state, action: PayloadAction<IBrowserSize>): void => {
            state.browserWidth = action.payload.browserWidth;
            state.browserHeight = action.payload.browserHeight;
    }
}
})



export const { setBrowserSize } = generalSlice.actions;
export default generalSlice;