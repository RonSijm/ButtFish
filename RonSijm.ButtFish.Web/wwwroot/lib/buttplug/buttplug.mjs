function U(s) {
  return s && s.__esModule && Object.prototype.hasOwnProperty.call(s, "default") ? s.default : s;
}
var k = { exports: {} }, A;
function j() {
  return A || (A = 1, (function(s) {
    var e = Object.prototype.hasOwnProperty, t = "~";
    function n() {
    }
    Object.create && (n.prototype = /* @__PURE__ */ Object.create(null), new n().__proto__ || (t = !1));
    function a(u, i, o) {
      this.fn = u, this.context = i, this.once = o || !1;
    }
    function g(u, i, o, c, v) {
      if (typeof o != "function")
        throw new TypeError("The listener must be a function");
      var p = new a(o, c || u, v), h = t ? t + i : i;
      return u._events[h] ? u._events[h].fn ? u._events[h] = [u._events[h], p] : u._events[h].push(p) : (u._events[h] = p, u._eventsCount++), u;
    }
    function l(u, i) {
      --u._eventsCount === 0 ? u._events = new n() : delete u._events[i];
    }
    function f() {
      this._events = new n(), this._eventsCount = 0;
    }
    f.prototype.eventNames = function() {
      var i = [], o, c;
      if (this._eventsCount === 0) return i;
      for (c in o = this._events)
        e.call(o, c) && i.push(t ? c.slice(1) : c);
      return Object.getOwnPropertySymbols ? i.concat(Object.getOwnPropertySymbols(o)) : i;
    }, f.prototype.listeners = function(i) {
      var o = t ? t + i : i, c = this._events[o];
      if (!c) return [];
      if (c.fn) return [c.fn];
      for (var v = 0, p = c.length, h = new Array(p); v < p; v++)
        h[v] = c[v].fn;
      return h;
    }, f.prototype.listenerCount = function(i) {
      var o = t ? t + i : i, c = this._events[o];
      return c ? c.fn ? 1 : c.length : 0;
    }, f.prototype.emit = function(i, o, c, v, p, h) {
      var I = t ? t + i : i;
      if (!this._events[I]) return !1;
      var r = this._events[I], M = arguments.length, y, d;
      if (r.fn) {
        switch (r.once && this.removeListener(i, r.fn, void 0, !0), M) {
          case 1:
            return r.fn.call(r.context), !0;
          case 2:
            return r.fn.call(r.context, o), !0;
          case 3:
            return r.fn.call(r.context, o, c), !0;
          case 4:
            return r.fn.call(r.context, o, c, v), !0;
          case 5:
            return r.fn.call(r.context, o, c, v, p), !0;
          case 6:
            return r.fn.call(r.context, o, c, v, p, h), !0;
        }
        for (d = 1, y = new Array(M - 1); d < M; d++)
          y[d - 1] = arguments[d];
        r.fn.apply(r.context, y);
      } else {
        var T = r.length, S;
        for (d = 0; d < T; d++)
          switch (r[d].once && this.removeListener(i, r[d].fn, void 0, !0), M) {
            case 1:
              r[d].fn.call(r[d].context);
              break;
            case 2:
              r[d].fn.call(r[d].context, o);
              break;
            case 3:
              r[d].fn.call(r[d].context, o, c);
              break;
            case 4:
              r[d].fn.call(r[d].context, o, c, v);
              break;
            default:
              if (!y) for (S = 1, y = new Array(M - 1); S < M; S++)
                y[S - 1] = arguments[S];
              r[d].fn.apply(r[d].context, y);
          }
      }
      return !0;
    }, f.prototype.on = function(i, o, c) {
      return g(this, i, o, c, !1);
    }, f.prototype.once = function(i, o, c) {
      return g(this, i, o, c, !0);
    }, f.prototype.removeListener = function(i, o, c, v) {
      var p = t ? t + i : i;
      if (!this._events[p]) return this;
      if (!o)
        return l(this, p), this;
      var h = this._events[p];
      if (h.fn)
        h.fn === o && (!v || h.once) && (!c || h.context === c) && l(this, p);
      else {
        for (var I = 0, r = [], M = h.length; I < M; I++)
          (h[I].fn !== o || v && !h[I].once || c && h[I].context !== c) && r.push(h[I]);
        r.length ? this._events[p] = r.length === 1 ? r[0] : r : l(this, p);
      }
      return this;
    }, f.prototype.removeAllListeners = function(i) {
      var o;
      return i ? (o = t ? t + i : i, this._events[o] && l(this, o)) : (this._events = new n(), this._eventsCount = 0), this;
    }, f.prototype.off = f.prototype.removeListener, f.prototype.addListener = f.prototype.on, f.prefixed = t, f.EventEmitter = f, s.exports = f;
  })(k)), k.exports;
}
var q = j();
const P = /* @__PURE__ */ U(q);
var V = /* @__PURE__ */ ((s) => (s[s.Off = 0] = "Off", s[s.Error = 1] = "Error", s[s.Warn = 2] = "Warn", s[s.Info = 3] = "Info", s[s.Debug = 4] = "Debug", s[s.Trace = 5] = "Trace", s))(V || {});
class J {
  /** Timestamp for the log message */
  timestamp;
  /** Log Message */
  logMessage;
  /** Log Level */
  logLevel;
  /**
   * @param logMessage Log message.
   * @param logLevel: Log severity level.
   */
  constructor(e, t) {
    const n = /* @__PURE__ */ new Date(), a = n.getHours(), g = n.getMinutes(), l = n.getSeconds();
    this.timestamp = `${a}:${g}:${l}`, this.logMessage = e, this.logLevel = t;
  }
  /**
   * Returns the log message.
   */
  get Message() {
    return this.logMessage;
  }
  /**
   * Returns the log message level.
   */
  get LogLevel() {
    return this.logLevel;
  }
  /**
   * Returns the log message timestamp.
   */
  get Timestamp() {
    return this.timestamp;
  }
  /**
   * Returns a formatted string with timestamp, level, and message.
   */
  get FormattedMessage() {
    return `${V[this.logLevel]} : ${this.timestamp} : ${this.logMessage}`;
  }
}
class b extends P {
  /** Singleton instance for the logger */
  static sLogger = void 0;
  /** Sets maximum log level to log to console */
  maximumConsoleLogLevel = 0;
  /** Sets maximum log level for all log messages */
  maximumEventLogLevel = 0;
  /**
   * Returns the stored static instance of the logger, creating one if it
   * doesn't currently exist.
   */
  static get Logger() {
    return b.sLogger === void 0 && (b.sLogger = new b()), this.sLogger;
  }
  /**
   * Constructor. Can only be called internally since we regulate ButtplugLogger
   * ownership.
   */
  constructor() {
    super();
  }
  /**
   * Set the maximum log level to output to console.
   */
  get MaximumConsoleLogLevel() {
    return this.maximumConsoleLogLevel;
  }
  /**
   * Get the maximum log level to output to console.
   */
  set MaximumConsoleLogLevel(e) {
    this.maximumConsoleLogLevel = e;
  }
  /**
   * Set the global maximum log level
   */
  get MaximumEventLogLevel() {
    return this.maximumEventLogLevel;
  }
  /**
   * Get the global maximum log level
   */
  set MaximumEventLogLevel(e) {
    this.maximumEventLogLevel = e;
  }
  /**
   * Log new message at Error level.
   */
  Error(e) {
    this.AddLogMessage(
      e,
      1
      /* Error */
    );
  }
  /**
   * Log new message at Warn level.
   */
  Warn(e) {
    this.AddLogMessage(
      e,
      2
      /* Warn */
    );
  }
  /**
   * Log new message at Info level.
   */
  Info(e) {
    this.AddLogMessage(
      e,
      3
      /* Info */
    );
  }
  /**
   * Log new message at Debug level.
   */
  Debug(e) {
    this.AddLogMessage(
      e,
      4
      /* Debug */
    );
  }
  /**
   * Log new message at Trace level.
   */
  Trace(e) {
    this.AddLogMessage(
      e,
      5
      /* Trace */
    );
  }
  /**
   * Checks to see if message should be logged, and if so, adds message to the
   * log buffer. May also print message and emit event.
   */
  AddLogMessage(e, t) {
    if (t > this.maximumEventLogLevel && t > this.maximumConsoleLogLevel)
      return;
    const n = new J(e, t);
    t <= this.maximumConsoleLogLevel && console.log(n.FormattedMessage), t <= this.maximumEventLogLevel && this.emit("log", n);
  }
}
class m extends Error {
  get ErrorClass() {
    return this.errorClass;
  }
  get InnerError() {
    return this.innerError;
  }
  get Id() {
    return this.messageId;
  }
  get ErrorMessage() {
    return {
      Error: {
        Id: this.Id,
        ErrorCode: this.ErrorClass,
        ErrorMessage: this.message
      }
    };
  }
  static LogAndError(e, t, n, a = C) {
    return t.Error(n), new e(n, a);
  }
  static FromError(e) {
    switch (e.ErrorCode) {
      case w.ERROR_DEVICE:
        return new E(e.ErrorMessage, e.Id);
      case w.ERROR_INIT:
        return new G(e.ErrorMessage, e.Id);
      case w.ERROR_UNKNOWN:
        return new z(e.ErrorMessage, e.Id);
      case w.ERROR_PING:
        return new K(e.ErrorMessage, e.Id);
      case w.ERROR_MSG:
        return new R(e.ErrorMessage, e.Id);
      default:
        throw new Error(`Message type ${e.ErrorCode} not handled`);
    }
  }
  errorClass = w.ERROR_UNKNOWN;
  innerError;
  messageId;
  constructor(e, t, n = C, a) {
    super(e), this.errorClass = t, this.innerError = a, this.messageId = n;
  }
}
class G extends m {
  constructor(e, t = C) {
    super(e, w.ERROR_INIT, t);
  }
}
class E extends m {
  constructor(e, t = C) {
    super(e, w.ERROR_DEVICE, t);
  }
}
class R extends m {
  constructor(e, t = C) {
    super(e, w.ERROR_MSG, t);
  }
}
class K extends m {
  constructor(e, t = C) {
    super(e, w.ERROR_PING, t);
  }
}
class z extends m {
  constructor(e, t = C) {
    super(e, w.ERROR_UNKNOWN, t);
  }
}
const C = 0, oe = 1, ae = 4294967295, H = 4, X = 0;
function F(s) {
  for (let [e, t] of Object.entries(s))
    if (t != null)
      return t.Id;
  throw new R(`Message ${s} does not have an ID.`);
}
function Y(s, e) {
  for (let [t, n] of Object.entries(s))
    if (n != null) {
      n.Id = e;
      return;
    }
  throw new R(`Message ${s} does not have an ID.`);
}
var w = /* @__PURE__ */ ((s) => (s[s.ERROR_UNKNOWN = 0] = "ERROR_UNKNOWN", s[s.ERROR_INIT = 1] = "ERROR_INIT", s[s.ERROR_PING = 2] = "ERROR_PING", s[s.ERROR_MSG = 3] = "ERROR_MSG", s[s.ERROR_DEVICE = 4] = "ERROR_DEVICE", s))(w || {}), _ = /* @__PURE__ */ ((s) => (s.Unknown = "Unknown", s.Vibrate = "Vibrate", s.Rotate = "Rotate", s.Oscillate = "Oscillate", s.Constrict = "Constrict", s.Inflate = "Inflate", s.Position = "Position", s.PositionWithDuration = "PositionWithDuration", s.Temperature = "Temperature", s.Spray = "Spray", s.Led = "Led", s))(_ || {}), D = /* @__PURE__ */ ((s) => (s.Unknown = "Unknown", s.Battery = "Battery", s.RSSI = "RSSI", s.Button = "Button", s.Pressure = "Pressure", s))(D || {}), L = /* @__PURE__ */ ((s) => (s.Read = "Read", s.Subscribe = "Subscribe", s.Unsubscribe = "Unsubscribe", s))(L || {});
class Q {
  constructor(e, t, n, a) {
    this._deviceIndex = e, this._deviceName = t, this._feature = n, this._sendClosure = a;
  }
  send = async (e) => await this._sendClosure(e);
  sendMsgExpectOk = async (e) => {
    const t = await this.send(e);
    if (t.Ok === void 0)
      throw t.Error !== void 0 ? m.FromError(t) : new R("Expected Ok or Error, and didn't get either!");
  };
  isOutputValid(e) {
    if (this._feature.Output !== void 0 && !this._feature.Output.hasOwnProperty(e))
      throw new E(`Feature index ${this._feature.FeatureIndex} does not support type ${e} for device ${this._deviceName}`);
  }
  isInputValid(e) {
    if (this._feature.Input !== void 0 && !this._feature.Input.hasOwnProperty(e))
      throw new E(`Feature index ${this._feature.FeatureIndex} does not support type ${e} for device ${this._deviceName}`);
  }
  async sendOutputCmd(e) {
    if (this.isOutputValid(e.outputType), e.value === void 0)
      throw new E(`${e.outputType} requires value defined`);
    let t = e.outputType, n;
    if (t == _.PositionWithDuration) {
      if (e.duration === void 0)
        throw new E("PositionWithDuration requires duration defined");
      n = e.duration;
    }
    let a, g = e.value;
    g.percent === void 0 ? a = e.value.steps : a = Math.ceil(this._feature.Output[t].Value[1] * g.percent);
    let l = { Value: a, Duration: n }, f = {};
    f[t.toString()] = l;
    let u = {
      OutputCmd: {
        Id: 1,
        DeviceIndex: this._deviceIndex,
        FeatureIndex: this._feature.FeatureIndex,
        Command: f
      }
    };
    await this.sendMsgExpectOk(u);
  }
  hasOutput(e) {
    return this._feature.Output !== void 0 ? this._feature.Output.hasOwnProperty(e.toString()) : !1;
  }
  hasInput(e) {
    return this._feature.Input !== void 0 ? this._feature.Input.hasOwnProperty(e.toString()) : !1;
  }
  async runOutput(e) {
    if (this._feature.Output !== void 0 && this._feature.Output.hasOwnProperty(e.outputType.toString()))
      return this.sendOutputCmd(e);
    throw new E(`Output type ${e.outputType} not supported by feature.`);
  }
  async runInput(e, t) {
    this.isInputValid(e);
    let n = this._feature.Input[e];
    if (console.log(this._feature.Input), t === L.Unsubscribe && !n.Command.includes(L.Subscribe) && !n.Command.includes(t))
      throw new E(`${e} does not support command ${t}`);
    let a = {
      InputCmd: {
        Id: 1,
        DeviceIndex: this._deviceIndex,
        FeatureIndex: this._feature.FeatureIndex,
        Type: e,
        Command: t
      }
    };
    if (t == L.Read) {
      const g = await this.send(a);
      if (g.InputReading !== void 0)
        return g.InputReading;
      throw g.Error !== void 0 ? m.FromError(g) : new R("Expected InputReading or Error, and didn't get either!");
    } else
      console.log(`Sending subscribe message: ${JSON.stringify(a)}`), await this.sendMsgExpectOk(a), console.log("Got back ok?");
  }
}
class B extends P {
  //
  //  // Map of messages and their attributes (feature count, etc...)
  //  private allowedMsgs: Map<string, Messages.MessageAttributes> = new Map<
  //    string,
  //    Messages.MessageAttributes
  //  >();
  //
  /**
   * @param _index Index of the device, as created by the device manager.
   * @param _name Name of the device.
   * @param allowedMsgs Buttplug messages the device can receive.
   */
  constructor(e, t) {
    super(), this._deviceInfo = e, this._sendClosure = t, this._features = new Map(Object.entries(e.DeviceFeatures).map(([n, a]) => [parseInt(n), new Q(e.DeviceIndex, e.DeviceName, a, t)]));
  }
  _features;
  /**
   * Return the name of the device.
   */
  get name() {
    return this._deviceInfo.DeviceName;
  }
  /**
   * Return the user set name of the device.
   */
  get displayName() {
    return this._deviceInfo.DeviceDisplayName;
  }
  /**
   * Return the index of the device.
   */
  get index() {
    return this._deviceInfo.DeviceIndex;
  }
  /**
   * Return the index of the device.
   */
  get messageTimingGap() {
    return this._deviceInfo.DeviceMessageTimingGap;
  }
  get features() {
    return this._features;
  }
  //  /**
  //   * Return a list of message types the device accepts.
  //   */
  //  public get messageAttributes(): Messages.MessageAttributes {
  //    return this._deviceInfo.DeviceMessages;
  //  }
  //
  static fromMsg(e, t) {
    return new B(e, t);
  }
  async send(e) {
    return await this._sendClosure(e);
  }
  sendMsgExpectOk = async (e) => {
    const t = await this.send(e);
    if (t.Ok === void 0 && t.Error !== void 0)
      throw m.FromError(t);
  };
  isOutputValid(e, t) {
    if (!this._deviceInfo.DeviceFeatures.hasOwnProperty(e.toString()))
      throw new E(`Feature index ${e} does not exist for device ${this.name}`);
    if (this._deviceInfo.DeviceFeatures[e.toString()].Outputs !== void 0 && !this._deviceInfo.DeviceFeatures[e.toString()].Outputs.hasOwnProperty(t))
      throw new E(`Feature index ${e} does not support type ${t} for device ${this.name}`);
  }
  hasOutput(e) {
    return this._features.values().filter((t) => t.hasOutput(e)).toArray().length > 0;
  }
  hasInput(e) {
    return this._features.values().filter((t) => t.hasInput(e)).toArray().length > 0;
  }
  async runOutput(e) {
    let t = [];
    for (let n of this._features.values())
      n.hasOutput(e.outputType) && t.push(n.runOutput(e));
    if (t.length == 0)
      return Promise.reject(`No features with output type ${e.outputType}`);
    await Promise.all(t);
  }
  async stop() {
    await this.sendMsgExpectOk({ StopDeviceCmd: { Id: 1, DeviceIndex: this.index } });
  }
  async battery() {
    for (let e of this._features.values())
      if (e.hasInput(D.Battery)) {
        let t = await e.runInput(D.Battery, L.Read);
        if (t === void 0)
          throw new R("Got incorrect message back.");
        if (t.Reading[D.Battery] === void 0)
          throw new R("Got reading with no Battery info.");
        return t.Reading[D.Battery].Value;
      }
    throw new E("No battery present on this device.");
  }
  emitDisconnected() {
    this.emit("deviceremoved");
  }
}
class Z {
  constructor(e) {
    this._useCounter = e;
  }
  _counter = 1;
  _waitingMsgs = /* @__PURE__ */ new Map();
  // One of the places we should actually return a promise, as we need to store
  // them while waiting for them to return across the line.
  // tslint:disable:promise-function-async
  PrepareOutgoingMessage(e) {
    this._useCounter && (Y(e, this._counter), this._counter += 1);
    let t, n;
    const a = new Promise(
      (g, l) => {
        t = g, n = l;
      }
    );
    return this._waitingMsgs.set(F(e), [t, n]), a;
  }
  ParseIncomingMessages(e) {
    const t = [];
    for (const n of e) {
      let a = F(n);
      if (a !== C && this._waitingMsgs.has(a)) {
        const [g, l] = this._waitingMsgs.get(a);
        if (n.Error !== void 0) {
          l(m.FromError(n.Error));
          continue;
        }
        g(n);
        continue;
      } else
        t.push(n);
    }
    return t;
  }
}
class ee extends m {
  constructor(e) {
    super(e, w.ERROR_UNKNOWN);
  }
}
class ce extends P {
  _pingTimer = null;
  _connector = null;
  _devices = /* @__PURE__ */ new Map();
  _clientName;
  _logger = b.Logger;
  _isScanning = !1;
  _sorter = new Z(!0);
  constructor(e = "Generic Buttplug Client") {
    super(), this._clientName = e, this._logger.Debug(`ButtplugClient: Client ${e} created.`);
  }
  get connected() {
    return this._connector !== null && this._connector.Connected;
  }
  get devices() {
    return this.checkConnector(), this._devices;
  }
  get isScanning() {
    return this._isScanning;
  }
  connect = async (e) => {
    this._logger.Info(
      `ButtplugClient: Connecting using ${e.constructor.name}`
    ), await e.connect(), this._connector = e, this._connector.addListener("message", this.parseMessages), this._connector.addListener("disconnect", this.disconnectHandler), await this.initializeConnection();
  };
  disconnect = async () => {
    this._logger.Debug("ButtplugClient: Disconnect called"), this.checkConnector(), await this.shutdownConnection(), await this._connector.disconnect();
  };
  startScanning = async () => {
    this._logger.Debug("ButtplugClient: StartScanning called"), this._isScanning = !0, await this.sendMsgExpectOk({ StartScanning: { Id: 1 } });
  };
  stopScanning = async () => {
    this._logger.Debug("ButtplugClient: StopScanning called"), this._isScanning = !1, await this.sendMsgExpectOk({ StopScanning: { Id: 1 } });
  };
  stopAllDevices = async () => {
    this._logger.Debug("ButtplugClient: StopAllDevices"), await this.sendMsgExpectOk({ StopAllDevices: { Id: 1 } });
  };
  disconnectHandler = () => {
    this._logger.Info("ButtplugClient: Disconnect event receieved."), this.emit("disconnect");
  };
  parseMessages = (e) => {
    const t = this._sorter.ParseIncomingMessages(e);
    for (const n of t)
      if (n.DeviceList !== void 0) {
        this.parseDeviceList(n);
        break;
      } else n.ScanningFinished !== void 0 ? (this._isScanning = !1, this.emit("scanningfinished", n)) : console.log(JSON.stringify(e));
  };
  initializeConnection = async () => {
    this.checkConnector();
    const e = await this.sendMessage(
      {
        RequestServerInfo: {
          ClientName: this._clientName,
          Id: 1,
          ProtocolVersionMajor: H,
          ProtocolVersionMinor: X
        }
      }
    );
    if (e.ServerInfo !== void 0) {
      const t = e;
      return this._logger.Info(
        `ButtplugClient: Connected to Server ${t.ServerName}`
      ), t.MaxPingTime, await this.requestDeviceList(), !0;
    } else if (e.Error !== void 0) {
      await this._connector.disconnect();
      const t = e.Error;
      throw m.LogAndError(
        G,
        this._logger,
        `Cannot connect to server. ${t.ErrorMessage}`
      );
    }
    return !1;
  };
  parseDeviceList = (e) => {
    for (let [t, n] of Object.entries(e.Devices))
      if (this._devices.has(n.DeviceIndex))
        this._logger.Debug(`ButtplugClient: Device already added: ${n}`);
      else {
        const a = B.fromMsg(
          n,
          this.sendMessageClosure
        );
        this._logger.Debug(`ButtplugClient: Adding Device: ${a}`), this._devices.set(n.DeviceIndex, a), this.emit("deviceadded", a);
      }
    for (let [t, n] of this._devices.entries())
      e.Devices.hasOwnProperty(t.toString()) || (this._devices.delete(t), this.emit("deviceremoved", n));
  };
  requestDeviceList = async () => {
    this.checkConnector(), this._logger.Debug("ButtplugClient: ReceiveDeviceList called");
    const e = await this.sendMessage(
      {
        RequestDeviceList: { Id: 1 }
      }
    );
    this.parseDeviceList(e.DeviceList);
  };
  shutdownConnection = async () => {
    await this.stopAllDevices(), this._pingTimer !== null && (clearInterval(this._pingTimer), this._pingTimer = null);
  };
  async sendMessage(e) {
    this.checkConnector();
    const t = this._sorter.PrepareOutgoingMessage(e);
    return await this._connector.send(e), await t;
  }
  checkConnector() {
    if (!this.connected)
      throw new ee(
        "ButtplugClient not connected"
      );
  }
  sendMsgExpectOk = async (e) => {
    const t = await this.sendMessage(e);
    if (t.Ok === void 0)
      throw t.Error !== void 0 ? m.FromError(t) : m.LogAndError(
        R,
        this._logger,
        `Message ${t} not handled by SendMsgExpectOk`
      );
  };
  sendMessageClosure = async (e) => await this.sendMessage(e);
}
class te extends P {
  constructor(e) {
    super(), this._url = e;
  }
  _ws;
  _websocketConstructor = null;
  get Connected() {
    return this._ws !== void 0;
  }
  connect = async () => new Promise((e, t) => {
    const n = new (this._websocketConstructor ?? WebSocket)(this._url), a = (l) => {
      t(l);
    }, g = (l) => t(l.reason);
    n.addEventListener("open", async () => {
      this._ws = n;
      try {
        await this.initialize(), this._ws.addEventListener("message", (l) => {
          this.parseIncomingMessage(l);
        }), this._ws.removeEventListener("close", g), this._ws.removeEventListener("error", a), this._ws.addEventListener("close", this.disconnect), e();
      } catch (l) {
        t(l);
      }
    }), n.addEventListener("error", a), n.addEventListener("close", g);
  });
  disconnect = async () => {
    this.Connected && (this._ws.close(), this._ws = void 0, this.emit("disconnect"));
  };
  sendMessage(e) {
    if (!this.Connected)
      throw new Error("ButtplugBrowserWebsocketConnector not connected");
    this._ws.send("[" + JSON.stringify(e) + "]");
  }
  initialize = async () => Promise.resolve();
  parseIncomingMessage(e) {
    if (typeof e.data == "string") {
      const t = JSON.parse(e.data);
      this.emit("message", t);
    } else e.data instanceof Blob;
  }
  onReaderLoad(e) {
    const t = JSON.parse(e.target.result);
    this.emit("message", t);
  }
}
class se extends te {
  send = (e) => {
    if (!this.Connected)
      throw new Error("ButtplugClient not connected");
    this.sendMessage(e);
  };
}
var $, W;
function ne() {
  return W || (W = 1, $ = function() {
    throw new Error(
      "ws does not work in the browser. Browser clients must use the native WebSocket object"
    );
  }), $;
}
var re = ne();
class ue extends se {
  _websocketConstructor = re.WebSocket;
}
class x {
  _percent;
  _steps;
  get percent() {
    return this._percent;
  }
  get steps() {
    return this._steps;
  }
  static createSteps(e) {
    let t = new x();
    return t._steps = e, t;
  }
  static createPercent(e) {
    if (e < 0 || e > 1)
      throw new E(`Percent value ${e} is not in the range 0.0 <= x <= 1.0`);
    let t = new x();
    return t._percent = e, t;
  }
}
class N {
  constructor(e, t, n) {
    this._outputType = e, this._value = t, this._duration = n;
  }
  get outputType() {
    return this._outputType;
  }
  get value() {
    return this._value;
  }
  get duration() {
    return this._duration;
  }
}
class O {
  constructor(e) {
    this._outputType = e;
  }
  steps(e) {
    return new N(this._outputType, x.createSteps(e), void 0);
  }
  percent(e) {
    return new N(this._outputType, x.createPercent(e), void 0);
  }
}
class ie {
  steps(e, t) {
    return new N(_.Position, x.createSteps(e), t);
  }
  percent(e, t) {
    return new N(_.PositionWithDuration, x.createPercent(e), t);
  }
}
class he {
  constructor() {
  }
  static get Vibrate() {
    return new O(_.Vibrate);
  }
  static get Rotate() {
    return new O(_.Rotate);
  }
  static get Oscillate() {
    return new O(_.Oscillate);
  }
  static get Constrict() {
    return new O(_.Constrict);
  }
  static get Inflate() {
    return new O(_.Inflate);
  }
  static get Temperature() {
    return new O(_.Temperature);
  }
  static get Led() {
    return new O(_.Led);
  }
  static get Spray() {
    return new O(_.Spray);
  }
  static get Position() {
    return new O(_.Position);
  }
  static get PositionWithDuration() {
    return new ie();
  }
}
export {
  se as ButtplugBrowserWebsocketClientConnector,
  ce as ButtplugClient,
  ee as ButtplugClientConnectorException,
  B as ButtplugClientDevice,
  E as ButtplugDeviceError,
  m as ButtplugError,
  G as ButtplugInitError,
  V as ButtplugLogLevel,
  b as ButtplugLogger,
  R as ButtplugMessageError,
  Z as ButtplugMessageSorter,
  ue as ButtplugNodeWebsocketClientConnector,
  K as ButtplugPingError,
  z as ButtplugUnknownError,
  oe as DEFAULT_MESSAGE_ID,
  he as DeviceOutput,
  N as DeviceOutputCommand,
  ie as DeviceOutputPositionWithDurationConstructor,
  O as DeviceOutputValueConstructor,
  w as ErrorClass,
  L as InputCommandType,
  D as InputType,
  J as LogMessage,
  ae as MAX_ID,
  H as MESSAGE_SPEC_VERSION_MAJOR,
  X as MESSAGE_SPEC_VERSION_MINOR,
  _ as OutputType,
  C as SYSTEM_MESSAGE_ID,
  F as msgId,
  Y as setMsgId
};
