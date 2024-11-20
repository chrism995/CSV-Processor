import { create } from "zustand";

interface MeterReadingState {
  successCount: number | null;
  failureCount: number | null;
  setMeterReadingResult: (success: number, failure: number) => void;
}

const useMeterReadingStore = create<MeterReadingState>((set) => ({
  successCount: null,
  failureCount: null,
  setMeterReadingResult: (success, failure) =>
    set({ successCount: success, failureCount: failure }),
}));

export default useMeterReadingStore;
