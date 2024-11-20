import { create } from "zustand";

interface MeterReading {
  accountId: number;
  meterReadingDateTime: string;
  meterReadValue: number;
}

interface MeterReadingState {
  meterReadings: MeterReading[];
  setMeterReadings: (readings: MeterReading[]) => void;
}

const useMeterReadingStore = create<MeterReadingState>((set) => ({
  meterReadings: [],
  setMeterReadings: (readings) => set({ meterReadings: readings }),
}));

export default useMeterReadingStore;
