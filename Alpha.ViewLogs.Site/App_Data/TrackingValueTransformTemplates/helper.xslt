<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xslLogFormat="urn:LogFormat">
  <xsl:variable name="isShowEmptyRow" select="xslLogFormat:ShowEmptyCell()" />

  <xsl:template name="renderIP">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'ip'" />
        <xsl:with-param name="firstValue" select="@ip" />
      </xsl:call-template>
    </table>
  </xsl:template>
  
  <xsl:template name="renderSubaccAndIP">
    <table class="tblDesc">
      <xsl:call-template name="renderRow">
        <xsl:with-param name="firstKey" select="'SubAccount'" />
        <xsl:with-param name="firstValue" select="@subaccount" />

        <xsl:with-param name="secondKey" select="'IP'" />
        <xsl:with-param name="secondValue" select="xslLogFormat:FormatIP(@ip)" />
      </xsl:call-template>
    </table>
  </xsl:template>
  
  <xsl:template name="renderRow">
    <xsl:param name="firstKey"/>
    <xsl:param name="firstValue"/>

    <xsl:param name="secondKey" select="''"/>
    <xsl:param name="secondValue"/>

    <xsl:param name="thirdKey" select="''"/>
    <xsl:param name="thirdValue"/>
    
    <tr>
      <xsl:call-template name="renderPairKeyValueCells">
        <xsl:with-param name="key" select="$firstKey" />
        <xsl:with-param name="value" select="$firstValue" />
      </xsl:call-template>

      <xsl:call-template name="renderPairKeyValueCells">
        <xsl:with-param name="key" select="$secondKey" />
        <xsl:with-param name="value" select="$secondValue" />
      </xsl:call-template>

      <xsl:call-template name="renderPairKeyValueCells">
        <xsl:with-param name="key" select="$thirdKey" />
        <xsl:with-param name="value" select="$thirdValue" />
      </xsl:call-template>
    </tr>
  </xsl:template>

  <xsl:template name="renderPairKeyValueCells">
    <xsl:param name="key"/>
    <xsl:param name="value"/>
    
    <xsl:if test="$isShowEmptyRow or $key != ''">
      <td class="colW1">
        <xsl:value-of select="xslLogFormat:GetLogResource($key)" />
      </td>
      <td class="colW2">
        <div>
          <xsl:value-of select="$value" />
        </div>
      </td>
    </xsl:if>
  </xsl:template>

  <xsl:template name="renderGeneralInformationRows">
    <tr class="action-name">
      <td colspan="6">
        <xsl:value-of select="xslLogFormat:GetLogResource('GeneralInformation')" />
      </td>
    </tr>
    <tr class="general-information">
      <td class="colW1">
        <xsl:value-of select="xslLogFormat:GetLogResource('subaccname')" />
      </td>
      <td class="colW2">
        <xsl:value-of select="@subacc" />
      </td>
      <td class="colW1">
        <xsl:value-of select="xslLogFormat:GetLogResource('IP')" />
      </td>
      <td class="colW2">
        <xsl:value-of select="xslLogFormat:FormatIP(@ip)" />
      </td>
      <xsl:if test="$isShowEmptyRow">
        <td class="colW1"></td>
        <td class="colW2"></td>
      </xsl:if>
    </tr>
  </xsl:template>

  <xsl:template name="renderSportNameRow">
    <xsl:param name="actionId" select="'0'"/>
    <tr class="action-name" onclick="slideToggle(this);">
      <td colspan="6">
        <xsl:value-of select="xslLogFormat:GetCN88SportName(@sport, $actionId)" />
      </td>
    </tr>
  </xsl:template>

  <xsl:template name="renderBettypeRow">
    <tr class="headerSportTypeDetail">
      <td colspan="6">
        <xsl:value-of select="xslLogFormat:GetCN88BettypeName(@sport, @bt, @inplay)" />
      </td>
    </tr>
  </xsl:template>
</xsl:stylesheet>
